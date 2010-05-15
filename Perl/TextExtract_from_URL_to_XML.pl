use strict;

use Encode;
use HTML::Parser;
use LWP::Simple;
use XML::DOM;
use XML::Twig;

our $html;
our $title;
our $keywords;
our $date;


#---------------------MAIN BODY BEGIN--------------------------
if (@ARGV != 1) {
	print "Usage: TextExtract_from_URL_to_XML.pl  URL-file.\n";
	exit(0);
}
open URL, $ARGV[0]	or die "can't open url file $ARGV[0]:$!\n";
while (<URL>) 
{
	next unless $_ =~ /^http/; 
	print $_;
	main($_);
	print $title." done!\n\n";
	Clear();
}
close(URL);
#---------------------MAIN BODY END----------------------------


sub Clear 
{
	$html = $title = $keywords = $date = "";
}


sub DownHTML
{
	my $url  = $_[0];
	my $flag = $_[1];
	$html = get($url) or die "unkown URL\n"; # get always use utf8
=pod
	# <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	my $encode = "";
	if ($html =~  m{meta\s+.*charset\s*=\s*(.+)["|'|\s*|/] }i ) {
		$encode = $1;
	}
	if ($encode eq  "utf-8") {
		$html = encode("gb2312", $html);
	} else {
		$html = encode("gb2312", $html);
	}
=cut
	$html = encode("gb2312", $html);
	while ($html =~ s/\r\n/\n/sg) {}
	if ($flag) 
	{
		open OUT, ">orig.html" or die "can't create out.html: $!\n";
		print OUT $html;
		close(OUT);
	}
	print "Downloading HTML done.\n";
}


sub PreProcess 
{
	if ($html =~ m{<title>(.*)</title>}si)  { $title = $1; }  # set title
	while ( $title =~ s/&.+?;//ig ) {}
	while ( $title =~ s{[\\|/|:|\*|\?|\"|<|>|\|]}{：}g ) {}   # filename can't use these tokens
	while ( $title =~ s/\s+/,/g ) {}

	# <meta name="keywords" content="李开复的博客,李开复,中西教育,自传,世界因你不同,教育" />
	if ($html =~ m{<meta\s+name="keywords"\s+content="?(.*?)["|/]}i)  { $keywords = $1; }

	my $date_flag = 0;
	# 2009-05-13  07:47
	if ($html =~ m/\d{4}-\d{2}-\d{2}(\s+\d{2}:\d{2})?/)  { $date = $&; $date_flag = 1; }

	# 2009年11月04日16:52
	if (! $date_flag) 
	{
		if ($html =~ m/\d{4}年\d{2}月\d{2}日(\s+\d{2}:\d{2})?/)	{ $date = $&; $date_flag = 1; }
	}

	# 2009/11/5 16:52
	if (! $date_flag) 
	{
		if ($html =~ m[\d{4}/\d{1,2}/\d{1,2}(\s+\d{2}:\d{2})?] ) { $date = $&; }
	}
	
	while ($html =~ s{<script.*?>.*?</script>}//isg) {}
	while ($html =~ s{<style.*?>.*?</style>}//isg)   {}
	while ($html =~ s/&.+?;/ /ig) {}
	open OUT, ">out.html" or die "can't create out.html: $!\n";
	print OUT $html;
	close(OUT);
	print "Pre Processing done.\n";
}


sub PostProcess 
{
	open IN, "text"	or die "can't open file text: $!\n";
	my @lines = <IN>;
	close(IN);

	my $BL = 3;
	my @index;
	open NN, ">num";
	for my $i (0 .. $#lines - $BL) 
	{
		my $words = 0;
		for (my $j = $i; $j < $i + $BL; $j++)  
		{ 
			$lines[$j] =~ s/\s+//g;
			$words += length($lines[$j]); 
		}
		push(@index, $words);
		print NN $words, "\n";
	}
	close(NN);

	my ($start, $end, $sflag, $eflag) = (-1, -1, 0, 0);
	for (my $i = 0; $i <= $#index; $i++)
	{
		if ($index[$i] > 100 && ! $sflag)
		{
			if ( ($index[$i]==$index[$i+1]) && ($index[$i+1]==$index[$i+2]) && ($index[$i+3] == 0) ) {$i+=2; next;} 

			if ($index[$i+1] || $index[$i+2] || $index[$i+3]) 
			{
				$sflag = 1;
				$start = $i;
				next;
			}
		}
		if ($sflag) 
		{
			if ($index[$i] == 0 && $index[$i+1] == 0) 
			{
				$end = $i;
				last;
			}
		}
	}
	
	print $start, "\t", $end, "\n";
	if ( ($end - $start == $BL && length($lines[$start]) < 100 && length($lines[$end-1]) < 100 )  # long single line text
		or ($start == -1) ) 
	{ 
		print "filter rule 1\n";
		open OUT, ">"."非正文网址_".$title.".xml"	or die "cant' write file {$title}.xml:$!\n";
		close(OUT);
		return; 
	}
	
	
	if ($end - $start < 20) 
	{
		my ($cnt, $cnt2) = (0, 0);
		for my $i ($start .. $end - 1) 
		{	
			if (length($lines[$i]) > 200) { goto Next; }
			if (length($lines[$i]) == 0) { $cnt2++; next; } 
			if (length($lines[$i]) < 60) { $cnt++; } 
		}
		print $cnt, "\t", $end-$start-$cnt2, "\t", $cnt / ($end-$start-$cnt2), "\n";
		if ( $cnt / ($end-$start-$cnt2) > 0.6 )
		{
			print "filter rule 2\n";
			open OUT, ">"."非正文网址_".$title.".xml"	or die "cant' write file {$title}.xml:$!\n";
			close(OUT);
			return;
		}
	}

Next:
=pod
	open OUT, ">".$title.".txt"	or die "cant' write file {$title}.txt:$!\n";
	print OUT "文章题目： ", $title, "\n";
	print OUT "关键字： ", $keywords, "\n";
	print OUT "发布日期： ", $date, "\n\n";
	print OUT "文章内容： ", "\n";
	for my $i ($start .. $end)
	{
		next if (length($lines[$i]) == 0);
		print OUT $lines[$i], "\n";
	}
	close(OUT);
	print "Post Processing done.\n";
=cut


	my $xml = "<Document/>";
	my $parser = new XML::DOM::Parser;
	my $dom = $parser->parse($xml);

	my $title_node = $dom->createElement("Title");
	my $title_c = $dom->createTextNode($title);
	$title_node->appendChild($title_c);
	$dom->getDocumentElement->appendChild($title_node);

	my $key_node = $dom->createElement("Keywords");
	my $key_c = $dom->createTextNode($keywords);
	$key_node->appendChild($key_c);
	$dom->getDocumentElement->appendChild($key_node);

	my $date_node = $dom->createElement("Date");
	my $date_c = $dom->createTextNode($date);
	$date_node->appendChild($date_c);
	$dom->getDocumentElement->appendChild($date_node);

	my $content = "";
	for my $i ($start .. $end)
	{
		next if (length($lines[$i]) == 0);
		$content .= $lines[$i]."\n";
	}
	my $content_node = $dom->createElement("Content");
	my $content_c = $dom->createTextNode($content);
	$content_node->appendChild($content_c);
	$dom->getDocumentElement->appendChild($content_node);

	my $twig = new XML::Twig; 
	$twig->set_indent(" "x4); 
	$twig->parse($dom->toString); 
	$twig->set_pretty_print("indented"); 

	open OUT, ">".$title.".xml"	or die "cant' write file {$title}.xml:$!\n";
	print OUT '<?xml version="1.0" encoding="gb2312"?>', "\n";
	print OUT $twig->sprint;
	close(OUT);	
	print "Post Processing done.\n";
}




sub main()
{	
	DownHTML($_[0], 1);
	PreProcess();

	open IN, "out.html"	or die "can't open file out.html: $!\n";
	open TEXT, ">text"	or die "can't write extracted text file: $!\n"; 

	my $p = HTML::Parser->new (api_version => 3,
			#start_h	   => [\&start, "tagname, attr, attrseq, text"],
			text_h	       => [\&text, "text"], );
			#end_h		   => [\&end, "tagname, text"], );

	sub text 
	{
		my ($text) = @_;
		print $text;
	}

	select(TEXT);  # IO redirection
	while (<IN>) 
	{
		$p->parse($_);
	}
	$p->eof;
	close(TEXT);
	select(STDOUT);


	PostProcess();
	close(IN);
	print "Main done.\n";
}




