<?php # Script - demo.php
	// set the running time limit of the script to 15 min
	set_time_limit( 60 * 15 );
	
	// set the directory of page file and extracted page text
	$sourceDir = 'pageFile/';
	$textDir = 'pageText/';
	
	// create the directory to store the useful information
	mkdir( $textDir );
	mkdir( 'mid' );
	mkdir( 'line_len' );
	mkdir( 'bl_size' );
	
	// set the number of file to be processed
	$fileNum = 10;
	$BL_BLOCK = 3;
	
	require_once( 'class.textExtract.php' );
	
	for( $j = 1; $j <= $fileNum; $j++ ) {
		// 获得网页内容
		$fileName = $sourceDir .  $j . '.htm';
		$content = file_get_contents( $fileName );
		
		$iTextExtractor = new textExtract( $content, $BL_BLOCK );
		$text = $iTextExtractor->getPlainText();
		
		$lineNum = count( $iTextExtractor->textLines );
		echo $lineNum . '<br />';
		
		// 输出预处理后得到的结果
		$midFileName = 'mid/mid_' . $j . '.txt';
		$fod = fopen( $midFileName, "w" );
		foreach( $iTextExtractor->textLines as $line ) {
			fprintf( $fod, "%s\n", $line );
		}
		fclose( $fod );
		
		// 输出经过预处理后每行的长度
		$lineLenFileName = 'line_len/line_len_' . $j . '.txt';
		$fod = fopen( $lineLenFileName, "w" );
		foreach( $iTextExtractor->textLines as $line ) {
			fprintf( $fod, "%s\n", strlen($line) );
		}
		fclose( $fod );
		
		// 输出行块内容的长度
		$blSizeFileName = 'bl_size/bl_size_' . $j . '.txt';
		$fptr = fopen( $blSizeFileName, "w" );
		foreach( $iTextExtractor->blksLen as $blkLen ) {
			fprintf( $fptr, "%d\n", $blkLen );
		}
		fclose( $fptr );
		
		// 输出正文内容
		$textFileName = $textDir . $j . '.html';
		$fod = fopen( $textFileName, "w" );
		fprintf( $fod, "%s\n", $text );
		fclose( $fod );
	}
?>