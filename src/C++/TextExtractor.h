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
 * ���ݳ����ڡ������п�ֲ�������ͨ����ҳ���ĳ�ȡ����������㷨��ʵ��C++�汾
 * 
 * @author ���� zhuliang@mail.software.ict.ac.cn
 * 2010��8��18��
 *
 */


/**
 * ��ʼ��������ʽ����
 *
 * ����ϵͳ��ʼ��ʱ����һ�����е�������ʽ
 */
void assign_regex();


/**
 * read the file content of the given filepath
 */
void load_file(std::string& s, std::istream& is);


/**
 * ɾ���ı��еĿհ׷�
 */
void removeSpace (std::string & str);



/**
 * ɾ��ԭʼHTML�ĵ��еĸ��ֱ�ǩ
 */
void removeTags  (std::string & HTML);


/**
 * ���ı��ָ��һ��һ�еĴ���һ��vector��
 */
void getAllLine (std::string & HTML);


/**
 * ͳ��������
 */
void statistic_line (std::string & HTML);


/**
 * ͳ���п鳤��
 */
void statistic_block (int blockWidth);


/**
 * �ҵ��п鳤�ȵ�һ��������ֵ���кš���������
 * 
 * @params	threshold	�п鳤�ȵ���ֵ
 * @return	�ҵ��ĵ�һ�����������ڵ��к�
 */
int findSurge (int start, int threshold);


/**
 * ����������ҵ���ǰ�Լ�β����п鳤�ȶ�Ϊ0�ĵ㡪���轵��
 * 
 * @params	surgePoint	������
 * @return	��������һ���轵�����ڵ��к�
 */
int findDive (int surgePoint);


void getContent (int start, int end, std::string & content);


int parse (const char* input_file, const char* output_file, int threshold);


#endif