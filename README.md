<p>The program follows the <span style="color: green">ClipboardFormatListener</span>. 
When text is copied to <span style="color: green">ClipBoard</span> then program check this text in 
<span style="color: yellow">Unicode</span> format and if text contans a special words or/and structure its trigger a function. 
Function retrieve a <span style="color: skyblue">HTML</span> version of text from <span style="color: green">ClipBoard</span>
then try to parse it by special rules for each separate block. At the moment the program can parse two general blocks:</p>
	<h3>First:</h3>
	<ul>
		<li>Preview - <span style="color: Khaki">Can be ingored</span>. 
		Rule: length of text between '&lt;h1&gt;' tag is less or equal to another '&lt;h1&gt;' tag, cannot be single;</li>
		<li>UTP - <span style="color: Khaki">Can be ingored</span>.
		Rule: count of words between '&lt;p&gt;' tags must be less or equal 5;</li>
		<li>SEO Table - Used to identify a header.
		Rule: must be in the table 3x2;</li>
		<li>Title - <span style="color: Khaki">Can be ingored</span>.
		Rule: length of text between '&lt;h1&gt;' tag is greater or equal to another '&lt;h1&gt;' tag or be single;</li>
		<li>Description - <span style="color: Khaki">Can be ingored</span>.
		Rule: count of words between '&lt;p&gt;' tags must be greater 5;</li>
		<li>Rewards - <span style="color: Khaki">Can be ingored</span>.
		Rule: each of '&lt;p&gt;' tags must be after '&lt;h2&gt;Rewards&lt;/h2&gt;';</li>
	</ul>
<h3>Second:</h3>
	<ul>
		<li>Requirements - <span style="color: Khaki">Can be ingored</span>. 
		Rule: all text in HTML format from '&lt;h2&gt;Requirements&lt;/h2&gt;' to '&lt;h2&gt;' tag or end of line;</li>
		<li>Additional Options - <span style="color: Khaki">Can be ingored</span>. 
		Rule: all text in HTML format from '&lt;h2&gt;Additional Options&lt;/h2&gt;' to '&lt;h2&gt;' tag or end of line;</li>
		<li>Boosting Method - <span style="color: Khaki">Can be ingored</span>. 
		Rule: all text in HTML format from '&lt;h2&gt;Boosting Method&lt;/h2&gt;' to '&lt;h2&gt;' tag or end of line;</li>
		<li>About - <span style="color: Khaki">Can be ingored</span>. 
		Rule: all text in HTML format from '&lt;h2&gt;About ...&lt;/h2&gt;' to '&lt;/h2&gt;' tag or end of line; 
		<span style="color: Indianred">*The word 'About' must be present to parse this block!</span></li>
		<li>FAQs - <span style="color: Khaki">Can be ingored</span>. 
		Rule: all text in HTML format from '&lt;h2&gt;FAQs&lt;/h2&gt;' to end of line;</li>
	</ul>

<span style="color: Indianred">
	<p>
		*Be careful with ignore because this function simply gives you the option to skip a block, 
		meaning that if it is skipped, the values that should have been replaced will still be replaced, 
		but with an empty value!
	</p>
	<p>
		*Also, be sure to select the entire list before copying, as this can lead to unpredictable behavior!
	</p>
</span>

<p>Next items that can be present in text is ignored/replaced/removed:</p>

<ul>
	<li>'H2 - ';</li>
	<li>'H3 - ';</li>
	<li>'[link!]';</li>
	<li>Any HTTP encoding characters are converted to their original state;</li>
	<li>'Footer' if a 'Header' anchor is detected. Has a warning in the console if this has happened;</li>
	<li>Any spaces and symbols in the word 'Self-Play' are replaced with 'Self-Play' due to the frequency of misspellings. 
	<span style="color: indianred">*The correct spelling of the words: 'self' and 'play' is still required;</span></li>
	<li>Typos such as:
		<ul>
			<li>Two or more 'spaces' in a row;</li>
			<li>Unbreakable 'spaces';</li>
			<li>'spaces' at the beginning and end of a line;</li>
		</ul>
	</li>
</ul>

<p>Any bold text, boolean or numeric lists, special characters, etc. are preserved during parsing.
So be sure to check this before using the script due to error frequency.</p>

<p>When parse is complete each part of it will be displayed in the program with colored text where 
'<span style="color: gray">gray</span>' - used for actions,
'<span style="color: green">green</span>' - used for success operation,
'<span style="color: red">red</span>' - used for warnings/errors.
Then <span style="color: chocolate">JavaScript</span> code which is correspond with parsed block is set to 
<span style="color: green">ClipBoard</span> and can be paste into <span style="color: orange">browser console</span> 
for execution.</p>