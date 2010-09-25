#ifndef nPatriciaTrieNODE_H
#define nPatriciaTrieNODE_H

#include <cstring>
#include <cstdlib>

typedef char* nPatriciaTrieKey;
template <class T> class nPatriciaTrie;
//----------------------------------------------------------------------------
//
// Class:   nPatriciaTrieNode
// Purpose: A node in a PATRICIA trie.
//          Each node stores one key, and the data associated with that key.
//
//----------------------------------------------------------------------------
template <class T>
class nPatriciaTrieNode {
	private:
		friend class nPatriciaTrie<T>;
		int bit_index;
		nPatriciaTrieNode<T>*   left;
		nPatriciaTrieNode<T>*   right;
		bool visit;

	public:
		nPatriciaTrieKey        key;
		T                       data;

		// Constructors & destructor
		nPatriciaTrieNode();
		nPatriciaTrieNode(nPatriciaTrieKey, T, int, nPatriciaTrieNode<T>*, nPatriciaTrieNode<T>*);
		virtual ~nPatriciaTrieNode();

		// Name:    Initialize
		// Args:    key, data, left, right
		// Return:  void
		// Purpose: Initialize this node with the given data.
		void		Initialize(nPatriciaTrieKey, T, int, nPatriciaTrieNode<T>*, nPatriciaTrieNode<T>*);

		// Name:	GetData/SetData
		// Args:	data : T
		// Return:	T | bool
		// Purpose:	Accessors for the data field.
		T			GetData();
		bool        SetData(T);
		
		// Name:	GetKey1
		// Args:	none
		// Return:	char*
		// Purpose:	Getter for the key field.
		nPatriciaTrieKey        GetKey();

		// Name:	GetLeft/GetRight
		// Args:	none
		// Return:	nPatriciaTrieNode*
		// Purpose:	Getters for the left/right fields.
		nPatriciaTrieNode<T>*   GetLeft();
		nPatriciaTrieNode<T>*   GetRight();
};

//----------------------------------------------------------------------------
template <class T>
nPatriciaTrieNode<T>::nPatriciaTrieNode() {
	Initialize(NULL, 0, -1, this, this);
}

//----------------------------------------------------------------------------
template <class T>
nPatriciaTrieNode<T>::nPatriciaTrieNode(nPatriciaTrieKey k,
                                        T d,
                                        int bi,
                                        nPatriciaTrieNode<T>* l,
                                        nPatriciaTrieNode<T>* r) {
    Initialize(k, d, bi, l, r);
}

//----------------------------------------------------------------------------
template <class T>
void nPatriciaTrieNode<T>::Initialize(nPatriciaTrieKey k,
                                      T d,
                                      int bi,
                                      nPatriciaTrieNode<T>* l,
                                      nPatriciaTrieNode<T>* r) {
	if (k)
		key = (nPatriciaTrieKey)strdup(k);
	else
		key = k;
	data      = d;
	left      = l;
	right     = r;
	bit_index = bi;
	visit	  = false;
}

//----------------------------------------------------------------------------
template <class T>
nPatriciaTrieNode<T>::~nPatriciaTrieNode() {
	if (key) {
		free(key);
		key = NULL;
	}
}

//----------------------------------------------------------------------------
template <class T>
T nPatriciaTrieNode<T>::GetData() {
	return data;
}

//----------------------------------------------------------------------------
template <class T>
bool nPatriciaTrieNode<T>::SetData(T d) {
	memcpy(&data, &d, sizeof(T));
	return true;
}

//----------------------------------------------------------------------------
template <class T>
nPatriciaTrieKey nPatriciaTrieNode<T>::GetKey() {
	return key;
}

//----------------------------------------------------------------------------
template <class T>
nPatriciaTrieNode<T>* nPatriciaTrieNode<T>::GetLeft() {
	return left;
}

//----------------------------------------------------------------------------
template <class T>
nPatriciaTrieNode<T>* nPatriciaTrieNode<T>::GetRight() {
	return right;
}



#endif
