<?php # Script - class.textExtract.php
/**
 * textExtract - text extraction class
 * Created on 2010-08-10
 * author: Wenfeng Xuan
 * Email: wfxuan@insun.hit.edu.cn
 * Blog: http://hi.baidu.com/xwf_like
 */
class textExtract {

	///////////////////////////////////
	// MEMBERS
	///////////////////////////////////
	
	/**
	 * record the web page's source code
	 * @var string
	 */
	public $rawPageCode = '';
	
	/**
	 * record the text after preprocessing
	 * @var array
	 */
	public $textLines   = array();
	
	/**
	 * record the length of each block
	 * @var array
	 */
	public $blksLen     = array();
	
	/**
	 * record the final extracted text
	 * @var string
	 */
	public $text        = '';
	
	/**
	 * set the size of each block ( regards how many single lines as a block )
	 * it is the only parameter of this method
	 * @var int
	 */
	public $blkSize;
	
	///////////////////////////////////
	// METHODS
	///////////////////////////////////
	
	/**
	 * Set the value of relevant members
	 * @param string $_rawPageCode
	 * @param int $_blkSize
	 * @return void
	 */
	function __construct( $_rawPageCode, $_blkSize = 3 ) {
		$this->rawPageCode = $_rawPageCode;
		$this->blkSize     = $_blkSize;
	}
	
	/**
	 * Preprocess the web page's source code
	 * @return string
	 */
	function preProcess() {
		$content = $this->rawPageCode;
		
		// 1. DTD information
		$pattern = '/<!DOCTYPE.*?>/si';
		$replacement = '';
		$content = preg_replace( $pattern, $replacement, $content );
		
		// 2. HTML comment
		$pattern = '/<!--.*?-->/s';
		$replacement = '';
		$content = preg_replace( $pattern, $replacement, $content );
		
		// 3. JavaScript
		$pattern = '/<script.*?>.*?<\/script>/si';
		$replacement = '';
		$content = preg_replace( $pattern, $replacement, $content );
		
		// 4. CSS
		$pattern = '/<style.*?>.*?<\/style>/si';
		$replacement = '';
		$content = preg_replace( $pattern, $replacement, $content );
		
		// 5. HTML TAGs
		$pattern = '/<.*?>/s';
		$replacement = '';
		$content = preg_replace( $pattern, $replacement, $content );
		
		// 6. some special charcaters
		$pattern = '/&.{1,5};|&#.{1,5};/';
		$replacement = '';
		$content = preg_replace( $pattern, $replacement, $content );
		
		return $content;
	}
	
	/**
	 * Split the preprocessed text into lines by '\n'
	 * after replacing "\r\n", '\n', and '\r' with '\n'
	 * @param string @rawText
	 * @return void
	 */
	function getTextLines( $rawText ) {
		// do some replacement
		$order = array( "\r\n", "\n", "\r" );
		$replace = '\n';
		$rawText = str_replace( $order, $replace, $rawText );
		
		$lines = explode( '\n', $rawText );
		
		foreach( $lines as $line ) {
			// remove the blanks in each line
			$tmp = preg_replace( '/\s+/s', '', $line );
			$this->textLines[] = $tmp;
		}
	}
	
	/**
	 * Calculate the blocks' length
	 * @return void
	 */
	function calBlocksLen() {
		$textLineNum = count( $this->textLines );
		
		// calculate the first block's length
		$blkLen = 0;
		for( $i = 0; $i < $this->blkSize; $i++ ) {
			$blkLen += strlen( $this->textLines[$i] );
		}
		$this->blksLen[] = $blkLen;
		
		// calculate the other block's length using Dynamic Programming method
		for( $i = 1; $i < ($textLineNum - $this->blkSize); $i++ ) {
			$blkLen = $this->blksLen[$i - 1] + strlen( $this->textLines[$i - 1 + $this->blkSize] ) - strlen( $this->textLines[$i - 1] );
			$this->blksLen[] = $blkLen;
		}
	}
	
	/**
	 * Extract the text from the web page's source code
	 * according to the simple idea:
	 * [the text should be the longgest continuous content
	 * in the web page]
	 * @return string
	 */
	function getPlainText() {
		$preProcText = $this->preProcess();
		$this->getTextLines( $preProcText );
		$this->calBlocksLen();
		
		$start = $end = -1;
		$i = $maxTextLen = 0;
		
		$blkNum = count( $this->blksLen );
		while( $i < $blkNum ) {
			while( ($i < $blkNum) && ($this->blksLen[$i] == 0) ) $i++;
			if( $i >= $blkNum ) break;
			$tmp = $i;
			
			$curTextLen = 0;
			$portion = '';
			while( ($i < $blkNum) && ($this->blksLen[$i] != 0) ) {
				if( $this->textLines[$i] != '' ) {
					$portion .= $this->textLines[$i];
					$portion .= '<br />';
					$curTextLen += strlen( $this->textLines[$i] );
				}
				$i++;
			}
			if( $curTextLen > $maxTextLen ) {
				$this->text = $portion;
				$maxTextLen = $curTextLen;
				$start = $tmp;
				$end = $i - 1;
			}
		}
		
		return $this->text;
	}
}
?>