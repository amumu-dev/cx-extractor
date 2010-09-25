#include "nPatriciaTrie.h"
#include <iostream>

template <class T>
bool LoadCorpus(const char* filename, nPatriciaTrie<T>* p)
{
	FILE* fp = fopen(filename, "r");
	if (!fp) return false;

	char ch[1024];
	int i;
	while(EOF != fscanf(fp, "%s\t%d\n", ch, &i))
	{
		p->Insert(ch, i);
	}
	cout << "done\n";
	return true;
}


int main(int argc, char* argv[]) {

	nPatriciaTrie<int>* p = new nPatriciaTrie<int>();
	const char* filepath = "./data/query_huge.txt";
	
	cout << "loading corpus...\t";
	if ( LoadCorpus(filepath, p) )
	{
		nPatriciaTrieNode<int>* root = p->head;

		PQueueINT  Q;

		char str;
		char ch[1024];
		do
		{
			cout << "enter keyword: ";
			cin >> ch;
			p->searchPrefix(Q, ch, 15);
			while (!Q.empty())
			{
				cout << Q.top()->key << ends << Q.top()->data << endl;
				Q.pop();
			}
			cout << "======================\n";
		} while (cout << "exit? y/n ", cin >> str, str == 'n');
	}


    /*
	p->Insert("SOME", 1);
	p->Insert("SOME", 1);
	p->Insert("SOME", 1);
	p->Insert("B", 1);
	p->Insert("THIS", 1);
	p->Insert("ABACUS", 1);
	p->Insert("ABRACADABRA", 1);
	p->Insert("SOMERSET", 1);
	p->Insert("SOMETHING", 1);
	p->Insert("ABRA", 1);
	p->Insert("ABRAB", 1);
	p->Insert("我是", 11);
	p->Insert("S我", 1);
	p->Insert("我爱", 1);
	p->Insert("我爱中国", 1111);
	p->Insert("我爱你", 19999);
	p->Insert("我", 1);
	p->Insert("1", 1);
	p->Insert("123", 1);
	
	p->Insert("ABC", 1);
	p->Insert("AB", 1);
	p->Insert("A", 1);
	p->Insert("AD", 1);
	p->Insert("D", 1);
	
	

	p->PreTraverse(root);
	cout << endl;
	p->InTraverse(root);
	cout << endl;
	p->PostTraverse(root);
	cout << endl;
	*/

	/*
    printf("Inserting... %s\n", p->Insert("foobar1", 1) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("foobar2", 2) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("foobar3", 3) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("foobar4", 4) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("foobar5", 5) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("__2867", 23) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("_23437256", 234) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("c:\\work\\development", -20) ? "OK" : "FAILED!");
	printf("Inserting... %s\n", p->Insert("c:\\work\\release", -22) ? "OK" : "FAILED!");

    // Lookup
    printf("foobar1 = %d\n", p->Lookup("foobar1"));
	printf("foobar2 = %d\n", p->Lookup("foobar2"));
	printf("foobar3 = %d\n", p->Lookup("foobar3"));
	printf("foobar4 = %d\n", p->Lookup("foobar4"));
	printf("foobar5 = %d\n", p->Lookup("foobar5"));
	printf("__2867 = %d\n", p->Lookup("__2867"));
	printf("_23437256 = %d\n", p->Lookup("_23437256"));
	printf("c:\\work\\development = %d\n", p->Lookup("c:\\work\\development"));
	printf("c:\\work\\release = %d\n", p->Lookup("c:\\work\\release"));

    // Remove some items from the structure
	printf("Deleting 'foobar4'... %s\n", p->Delete("foobar4") ? "OK" : "Uh-oh!");
	printf("Deleting 'foobar5'... %s\n", p->Delete("foobar5") ? "OK" : "Uh-oh!");

    // Lookup
    printf("Looking up 'foobar4'... %s\n", p->LookupNode("foobar4") ? "Still there!" : "Not there (OK).");
    printf("Looking up 'foobar5'... %s\n", p->LookupNode("foobar5") ? "Still there!" : "Not there (OK).");
	*/
    
	delete p;

	return 0;
}

