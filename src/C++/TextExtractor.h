#ifndef __TEXT_EXTRACTOR_
#define __TEXT_EXTRACTOR_

#include <fstream>
#include <sstream>
#include <string>
#include <iterator>
#include <boost/regex.hpp>
#include <fstream>
#include <iostream>
#include <ctype.h>


using namespace std;
using namespace boost;


/**
 * 根据陈鑫在《基于行块分布函数的通用网页正文抽取》中提出的算法，实现C++版本
 * 
 * @author 朱亮 zhuliang@mail.software.ict.ac.cn
 * 2010年8月18日
 *
 */


/**
 * 初始化正则表达式变量
 *
 * 仅在系统初始化时编译一次所有的正则表达式
 */
void assign_regex();


/**
 * read the file content of the given filepath
 */
void load_file(std::string& s, std::istream& is);


/**
 * 删除文本中的空白符
 */
void removeSpace (std::string & str);



/**
 * 删除原始HTML文档中的各种标签
 */
void removeTags  (std::string & HTML);


/**
 * 将文本分割成一行一行的存入一个vector中
 */
void getAllLine (std::string & HTML);


/**
 * 统计所有行
 */
void statistic_line (std::string & HTML);


/**
 * 统计行块长度
 */
void statistic_block (int blockWidth);


/**
 * 找到行块长度第一个超过阈值的行号——骤升点
 * 
 * @params	threshold	行块长度的阈值
 * @return	找到的第一个骤升点所在的行号
 */
int findSurge (int start, int threshold);


/**
 * 在骤升点后找到当前以及尾随的行块长度都为0的点——骤降点
 * 
 * @params	surgePoint	骤升点
 * @return	骤升点后第一个骤降点所在的行号
 */
int findDive (int surgePoint);


void getContent (int start, int end, std::string & content);


int parse (const char* input_file, const char* output_file, int threshold);


#endif