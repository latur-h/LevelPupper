Program is automaticaly triggered when the text with special anchors is copied. </br>Program has two main triggered events.</br>
First for title, seo and rewards and second for requirements, additional options, boosting method, about and faqs.</br>
</br>For first triggered events your text must match next structure:</br>
&nbsp;&nbsp;&nbsp;h1  "Preview"</br>
&nbsp;&nbsp;&nbsp;p "Utp"</br>
&nbsp;&nbsp;&nbsp;tb "Seo table"</br>
&nbsp;&nbsp;&nbsp;h1 "Title"</br>
&nbsp;&nbsp;&nbsp;p "Description"</br>
&nbsp;&nbsp;&nbsp;li "Rewards"</br>
</br>For second:</br>
&nbsp;&nbsp;&nbsp;h2 "Requirements"</br>
&nbsp;&nbsp;&nbsp;h2 "Additional Options"</br>
&nbsp;&nbsp;&nbsp;h2 "Boosting Method(s)"</br>
&nbsp;&nbsp;&nbsp;h2 "About *"</br>
&nbsp;&nbsp;&nbsp;h2 "FAQ(s)"</br>
</br>
When program match the input text and complete the parse you\`ll see the popup window with next message 
"Header is match!" - for first part and "Footer is match!" for second part </br> 
then javascript code is insert into clipboard and can be pasted into browser console to execution.
</br></br>
Also such things like "H2 - ", "H3 - " and "\[link!\]" is ignored. 
Important part for "bold" text, every boldness is automaticaly turns to \<strong> tags if the text is inside
\<p> or \<li> tags, also if text is inside "Boosting Method" this parameter is ignored \`cause this part is going into an element.
