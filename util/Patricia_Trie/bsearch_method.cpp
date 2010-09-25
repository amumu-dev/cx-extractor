#include <iostream>
#include <string>
#include <fstream>
#include <vector>
#include <algorithm>
using namespace std;


struct node
{
	node() {}
	node(string k, int f) : key(k), freq(f) {}
	string key;
	int freq;
};

class freq_cmp
{
public:
	bool operator() (const node& n1, const node& n2)
	{	return n1.freq > n2.freq;  }
};

class str_cmp
{
public:
	bool operator() (const node& n1, const node& n2)
	{	return (strcmp(n1.key.c_str(), n2.key.c_str()) <= 0) ? true : false;  }
};

bool LoadCorpus(const char* filename, vector<node>& vecAll)
{
	ifstream fin;
	fin.open(filename);
	if (!fin) return false;
	string query;
	int freq;
	while (fin >> query >> freq)
	{
		vecAll.push_back( node(query, freq) );
	}
	cout << "done\n";
	return true;
}


int pre_cmp(const string& s, const string& t)
{
	if (s == t.substr(0, s.size()) )  return 0; // s is t's prefix
	else return strcmp(s.c_str(), t.c_str());
}


int BinarySearch(string& word, const vector<node>& vec)
{
	int low = 0;
	int up	= vec.size() - 1;
	int mid = 0;
	while (low <= up)
	{
		mid = (low + up) / 2;
		int cmp = pre_cmp(word, vec[mid].key);
		if (cmp == 0) return mid;
		if (cmp < 0)  up = mid - 1;
		else low = mid + 1;
	}
	return -1;
}

void GetPrefix(vector<node>& prefix, const vector<node>& all, string& word)
{
	prefix.clear();
	int i = BinarySearch(word, all);
	if (-1 != i) // find a word with prefix 'word'
	{
		prefix.push_back(all[i]);
		// upward
		int j = i;
		while (j-1>=0 && 0 == pre_cmp(word, all[j-1].key) )
		{
			prefix.push_back(all[--j]);
		}
		// downward
		j = i;
		while (j+1<all.size() && 0 == pre_cmp(word, all[j+1].key) )
		{
			prefix.push_back(all[++j]);
		}
	}
}


int main()
{
	vector<node> vecAll;
	vector<node> vecPrefix;

	const string filepath = "./data/query_huge.txt";
	cout << "loading corpus...\t";
	
	if ( LoadCorpus(filepath.c_str(), vecAll) )
	{
		cout << "sorting " << vecAll.size() << " items!\n";
		sort(vecAll.begin(), vecAll.end(), str_cmp() );
	
		char ch;
		string word;
		do
		{	
			cout << "enter keyword: ";
			cin >> word;
			GetPrefix(vecPrefix, vecAll, word);
			sort(vecPrefix.begin(), vecPrefix.end(), freq_cmp() );
			cout << "find " << vecPrefix.size() << " items!\n";
			int limit = vecPrefix.size() > 10 ? 10 : vecPrefix.size();
			for (int i =0; i < limit; ++i)
				cout << vecPrefix[i].key << " " << vecPrefix[i].freq << endl;
			
			cout << "==========================\n";
		} while (cout << "exit? y/n ", cin >> ch, ch == 'n'); 
	}

	return 0;
}
