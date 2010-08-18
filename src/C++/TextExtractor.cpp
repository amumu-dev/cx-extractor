
#include	"TextExtractor.h"

boost::regex _doctype;
boost::regex _comment;
boost::regex _javascript;
boost::regex _css;
boost::regex _special_char;
boost::regex _other_tag;
boost::regex _empty_char;

std::vector<std::string> lines;
std::vector<int> blocks;

/**
 * 根据陈鑫在《基于行块分布函数的通用网页正文抽取》中提出的算法，实现C++版本
 * 
 * @author 朱亮 zhuliang@mail.software.ict.ac.cn
 * 2010年8月18日
 *
 */

void assign_regex()
{
	_doctype		= boost::regex ("(?is)<!DOCTYPE.*?>", boost::regex::icase|boost::regex::perl);
	_comment		= boost::regex ("(?is)<!--.*?-->", boost::regex::icase|boost::regex::perl);
	_javascript		= boost::regex ("(?is)<script.*?>.*?</script>", boost::regex::icase|boost::regex::perl);
	_css			= boost::regex ("(?is)<style.*?>.*?</style>", boost::regex::icase|boost::regex::perl);
	_special_char	= boost::regex ("&.{2,5};|&#.{2,5};", boost::regex::icase|boost::regex::perl);
	_other_tag		= boost::regex ("(?is)<.*?>", boost::regex::icase|boost::regex::perl);
	_empty_char		= boost::regex ("\\s", boost::regex::icase|boost::regex::perl);
	
}


void load_file(std::string& s, std::istream& is)
{
	s.erase();
	s.reserve(is.rdbuf()->in_avail());
	char c;
	while(is.get(c))
	{
		if(s.capacity() == s.size())
			s.reserve(s.capacity() * 3);
		s.append(1, c);
	}
	int l = s.size();
}


void removeSpace  (std::string & str)
{
	std::basic_string<char>::iterator iter = str.begin();
	while (iter != str.end())
	{
		//空格，制表符，换行符，回车符，换页符，报警符和转义符
		if ((' ' == (*iter)) || ('\t' == (*iter)) || ('\n' == (*iter)) || ('\r' == (*iter)) || ('\f' == (*iter)) || ('\a' == (*iter)) || ('\e' == (*iter)))
		{
			iter = str.erase(iter);
		}
		else
			iter++;
	}
}


void removeTags  (std::string & HTML)
{
	HTML = boost::regex_replace(HTML, _doctype, "");
	HTML = boost::regex_replace(HTML, _comment, "");
	HTML = boost::regex_replace(HTML, _javascript, "");
	HTML = boost::regex_replace(HTML, _css, "");
	HTML = boost::regex_replace(HTML, _special_char, "");
	HTML = boost::regex_replace(HTML, _other_tag, "");
}

int getOneLine (std::string & HTML, std::basic_string<char>::iterator & iter, std::string & line)
{
	line.clear();
	while( iter != HTML.end() && '\n' != (*iter))
	{
		line.push_back(*iter);
		iter++;
	}
	
	removeSpace (line);

	if (iter == HTML.end())	
		return 0;	
	else if ('\n' == (*iter))
	{
		iter++;
		return 1;
	}		
	else 
		return -1;
}

void getAllLine (std::string & HTML)
{
	lines.clear();

	std::string line;
	std::basic_string<char>::iterator iter;
	iter = HTML.begin();

	while (getOneLine(HTML, iter, line) > 0)
	{
		lines.push_back(line);
	}
}

void statistic_line (std::string & HTML)
{
	getAllLine (HTML);

	/*std::vector<std::string>::iterator it;
	it = lines.begin();
	while(it != lines.end())
	{
		std::cout << it->size() << '\n';
		it++;
	}*/
}

void statistic_block (int blockWidth)
{
	blocks.clear();

	for(int i=0; i < lines.size() - blockWidth; i++)
	{
		int block_len = 0;
		for (int j=0; j < blockWidth; j++)
			block_len += lines[i+j].length();
		blocks.push_back(block_len);
	}

	for(int i=0; i < blocks.size(); i++)
	{
		cout << blocks[i] << '\n';
	}
}

int findSurge (int start, int threshold)
{
	for(int i = start; i < blocks.size(); i++)
	{
		if ( blocks[i] > threshold 
			&& blocks[i+1] > 0			//紧随骤升点的行块长度不能为0
			&& blocks[i+2] > 0
			&& blocks[i+3] > 0 )
			return i;
	}
	return -1;
}

int findDive (int surgePoint)
{
	for (int i = surgePoint + 1; i < blocks.size(); i++)
	{
		if (blocks[i] == 0 && blocks[i+1] == 0)
			return i;
	}
	return blocks.size() -1;
}


void getContent (int start, int end, std::string & content)
{
	for(int i = start; i<= end; i ++)
	{
		content += lines[i];
		content.push_back('\n');
	}
}

int parse (const char* input_file, const char* output_file, int threshold)
{
	std::ifstream fs(input_file);
	std::string str;
	load_file(str, fs);

	assign_regex();

	removeTags(str);
	statistic_line (str);
	statistic_block (3);

	std::string content;
	int start = 0 ,end = 0;
	while (1)
	{
		start = findSurge (end, threshold);
		if (start < 0)
		{
			cout << "no more content has been found\n";
			break;
		}
		end = findDive(start);
	
		getContent(start, end, content);
	}
	
	ofstream outFile(output_file);
	outFile << content;
	outFile.close();
	return 0;
}


