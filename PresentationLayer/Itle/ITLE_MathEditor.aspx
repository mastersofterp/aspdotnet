<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title>Untitled Page</title>
<style type="text/css">


#latexbuttons .pane { margin-bottom: 3px;  border: 1px solid #aaa; }
#latexbuttons .pane .contents .padding { padding: 3px; }
#latexbuttons .pane .header {
  padding: 3px;
  text-transform: lowercase;
  background: #ccc;
  font-size: 90%;
  font-weight: bold;
}

#latexbuttons .pane .header a { text-decoration: none; }
#latexbuttons .pane .header a:hover { color: #666 }

#latexbuttons .pane .contents a {  text-decoration: none;  padding: 1px; display: inline-block; }
#latexbuttons .pane .contents a:hover {  opacity: 0.5; }
#latexbuttons .pane .contents a img {  border: none; }
#latexbuttons .pane .contents a img.blockimage {  margin: 0.5em; }

#latexbuttons hr {
  margin: 5px 0 5px 0;
  background-color: #ccc;
  border: 0;
  height: 1px;
}

textarea, #latexbuttons .pane {
  border-radius: 2px;
}

</style>
   
   <%--  <script src="JAVASCRIPTS/jquery.min_1.js" type="text/javascript"></script>

      <script src="JAVASCRIPTS/jquery-ui.min_1.js" type="text/javascript"></script>--%>
 
    


    <script src="/jquery/replacemath.js" type="text/javascript"></script>
  
      <script type="text/javascript">

          function getimage() {

             // alert(document.getElementById("txtimg").value);
              document.getElementById("getdata").innerHTML = '$$' + document.getElementById("latex").value + '$$';
              //alert(document.getElementById("getdata"));
              replaceMath(document.getElementById("getdata"));
          }
          
          
          
      </script>
  </head>

     <body>
  
  <form  runat ="server"> 
  
  <div style="height: 20px"> 
 
  
  <div id="getdata" style="width:100%"  >
        <%-- <img src="../IMAGES/loader.gif" alt="" />--%>
      </div>

   
  <%--<asp:Button ID="txtimge" runat="server" OnClientClick ="getimage();" />
  <asp:TextBox ID="txtimg"  runat ="server" > </asp:TextBox>
  --%>
  
   <%--<textarea name="latex" id="latex" rows="16" spellcheck="false" cols="80" accesskey="t"  onkeyup ="getimage();">\zeta(s) = \sum_{n=1}^\infty \frac{1}{n^s}</textarea>--%>
 
 <div style="width:99%" >
 <table width="70%">
 <tr>
 <td style ="height:30px;"> </td>
 </tr>
 <tr >
 <td style="width:50%;vertical-align:top;">
 <textarea  type ="text"  onkeyup="getimage();" id="latex" runat ="server" cols ="16"
  style="width:100%; height:300px;font-weight:bold;text-align:start"/>
 </td>
 <td style="width:2%"></td>
 <td style="width:50%">
 <asp:Panel ID="pnlbutton" runat="server" Height="300px" ScrollBars="Vertical">
 <div id="latexbuttons" style ="width:100%;float:right;vertical-align:super;">
        <div class="pane">
		<div class="header"><a href="#">Layout</a></div>
          <a href="#" class="insert" title="^{}" onclick="f1(this.title)"><img src="../IMAGES/sup.png" style="padding-bottom: 4px" alt="" /></a>&nbsp;
		  <a href="#" title="_{}" onclick="f1(this.title)"><img src="../IMAGES/sub.png" style="padding-top: 7px" alt="" /></a>&nbsp;
		  <a href="#" title="\frac{}{}" onclick="f1(this.title)"><img src="../IMAGES/frac.png" alt="" /></a>
       
       
          <a href="#" title="\begin{pmatrix}
&amp; \\
&amp; 
\end{pmatrix}" onclick="f1(this.title)"><img src="IMAGES/pmatrix.png" class="blockimage" alt="" /></a>
		  <a href="#" title="
\begin{align*}
&amp; =  \\
&amp; = 
\end{align*}" onclick="f1(this.title)"><img src="../IMAGES/align.png" class="blockimage" alt="" /></a>
		  <a href="#" title="
= \begin{cases}
 &amp; \text{if }  \\
 &amp; \text{if }  
\end{cases}" onclick="f1(this.title)"><img src="../IMAGES/cases.png" class="blockimage" alt="" /></a>
		  <hr />
		  <a href="#" title="\begin{itemize} % must be in paragraph mode
\item 
\item 
\end{itemize}" onclick="f1(this.title)"><img src="../IMAGES/itemize.png" class="blockimage" alt="" /></a>
		  <a href="#" title="\begin{enumerate} % must be in paragraph mode
\item 
\item 
\end{enumerate}" onclick="f1(this.title)"><img src="../IMAGES/enumerate.png" class="blockimage" alt="" /></a>
	      </div> 

          
       
         <div class="pane">
		<div class="header"><a href="#">Function</a></div>
       
          <a href="#" title="\frac{\partial}{\partial x}"  onclick="f1(this.title)"><img src="../IMAGES/partialpartialx.png"  alt="" /></a> &nbsp;&nbsp;&nbsp;
		  <a href="#" title="\frac{\text{d}}{\text{d}x}" onclick="f1(this.title)"><img src="../IMAGES/ddx.png" alt="" /></a>&nbsp;&nbsp;&nbsp;
		  <a href="#" title="\int_{a}^{b} f(x) \text{d}x" onclick="f1(this.title)"><img src="../IMAGES/intdx.png"  alt="" /></a>&nbsp;&nbsp;&nbsp;
		  <a href="#" title="\lim_{x \rightarrow a} f(x)" onclick="f1(this.title)"><img src="../IMAGES/limfx.png"  alt="" /></a>&nbsp;&nbsp;&nbsp;
        </div> 
        
        
         
         
	    <div class="pane">
	      <div class="header"><a href="#">Letters and Symbols</a></div>
	      <div class="contents"><div class="padding">
		<a  onclick="f1(this.title)" href="#" title="\alpha" >&alpha;</a>
		<a  onclick="f1(this.title)" href="#" title="\beta">&beta;</a>
		<a  onclick="f1(this.title)" href="#" title="\gamma">&gamma;</a>
		<a  onclick="f1(this.title)" href="#" title="\delta">&delta;</a>
		<a  onclick="f1(this.title)" href="#" title="\epsilon">&epsilon;</a>
		<a  onclick="f1(this.title)" href="#" title="\zeta">&zeta;</a>
		<a  onclick="f1(this.title)" href="#" title="\eta">&eta;</a>
		<a  onclick="f1(this.title)" href="#" title="\theta">&theta;</a>
		<a  onclick="f1(this.title)" href="#" title="\iota">&iota;</a>
		<a  onclick="f1(this.title)" href="#" title="\kappa">&kappa;</a>
		<a  onclick="f1(this.title)" href="#" title="\lambda">&lambda;</a>
		<a  onclick="f1(this.title)" href="#" title="\mu">&mu;</a>
		<a  onclick="f1(this.title)" href="#" title="\nu">&nu;</a>
		<a  onclick="f1(this.title)" href="#" title="\xi">&xi;</a>
		<a  onclick="f1(this.title)" href="#" title="\pi">&pi;</a>
		<a  onclick="f1(this.title)" href="#" title="\rho">&rho;</a>
		<a  onclick="f1(this.title)" href="#" title="\sigma">&sigma;</a>
		<a  onclick="f1(this.title)" href="#" title="\tau">&tau;</a>
		<a  onclick="f1(this.title)" href="#" title="\upsilon">&upsilon;</a>
		<a  onclick="f1(this.title)" href="#" title="\phi">&phi;</a>
		<a  onclick="f1(this.title)" href="#" title="\chi">&chi;</a>
		<a  onclick="f1(this.title)" href="#" title="\psi">&psi;</a>
		<a  onclick="f1(this.title)" href="#" title="\omega">&omega;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\Gamma">&Gamma;</a>
		<a  onclick="f1(this.title)" href="#" title="\Delta">&Delta;</a>
		<a  onclick="f1(this.title)" href="#" title="\Theta">&Theta;</a>
		<a  onclick="f1(this.title)" href="#" title="\Lambda">&Lambda;</a>
		<a  onclick="f1(this.title)" href="#" title="\Xi">&Xi;</a>
		<a  onclick="f1(this.title)" href="#" title="\Pi">&Pi;</a>
		<a  onclick="f1(this.title)" href="#" title="\Sigma">&Sigma;</a>
		<a  onclick="f1(this.title)" href="#" title="\Upsilon">&Upsilon;</a>
		<a  onclick="f1(this.title)" href="#" title="\Phi">&Phi;</a>
		<a  onclick="f1(this.title)" href="#" title="\Psi">&Psi;</a>
		<a  onclick="f1(this.title)" href="#" title="\Omega">&Omega;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\infty">&infin;</a>
		<a  onclick="f1(this.title)" href="#" title="\aleph">&alefsym;</a>
		<a  onclick="f1(this.title)" href="#" title="\hbar"><img src="../IMAGES/hbar.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\emptyset">&empty;</a>
		<a  onclick="f1(this.title)" href="#" title="\ell"><img src="../IMAGES/ell.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\angle">&ang;</a>
		<a  onclick="f1(this.title)" href="#" title="\mathbb{R}"><img src="../IMAGES/R.png" style="padding-bottom: 1px" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\mathbb{C}"><img src="../IMAGES/C.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\mathbb{Z}"><img src="../IMAGES/Z.png" style="padding-bottom: 1px" alt="" /></a>
	      </div></div>
	    </div>
	    <div class="pane">
	      <div class="header"><a href="#">Operators and Relations</a></div>
	      <div class="contents"><div class="padding">
		<a  onclick="f1(this.title)" href="#" title="\cdot">&sdot;</a>
		<a  onclick="f1(this.title)" href="#" title="\times">&times;</a>
 		<a  onclick="f1(this.title)" href="#" title="\div"><img src="../IMAGES/div.png" alt="" /></a>
 		<a  onclick="f1(this.title)" href="#" title="\surd">&radic;</a>
		<a  onclick="f1(this.title)" href="#" title="\pm"><img src="../IMAGES/pm.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\mp"><img src="../IMAGES/mp.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\nabla">&nabla;</a>
		<a  onclick="f1(this.title)" href="#" title="\partial">&part;</a>
		<hr />
 		<a  onclick="f1(this.title)" href="#" title="\sqrt{}"><img src="../IMAGES/sqrt.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\sum_{}^{}">&sum;</a>
		<a  onclick="f1(this.title)" href="#" title="\prod_{}^{}">&prod;</a>
		<a  onclick="f1(this.title)" href="#" title="\int_{}^{}">&int;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\neq">&ne;</a>
		<a  onclick="f1(this.title)" href="#" title="\leq">&le;</a>
		<a  onclick="f1(this.title)" href="#" title="\geq">&ge;</a>
		<a  onclick="f1(this.title)" href="#" title="\sim">&sim;</a>
		<a  onclick="f1(this.title)" href="#" title="\approx">&asymp;</a>
		<a  onclick="f1(this.title)" href="#" title="\cong">&cong;</a>
		<a  onclick="f1(this.title)" href="#" title="\equiv">&equiv;</a>
		<a  onclick="f1(this.title)" href="#" title="\propto">&prop;</a>
		<a  onclick="f1(this.title)" href="#" title="\ll"><img src="../IMAGES/ll.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\gg"><img src="../IMAGES/gg.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\implies">&rArr;</a>
		<a  onclick="f1(this.title)" href="#" title="\Leftrightarrow">&hArr;</a>
		<a  onclick="f1(this.title)" href="#" title="\rightleftharpoons"><img src="../IMAGES/harpoons.png" alt="" /></a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\in">&isin;</a>
		<a  onclick="f1(this.title)" href="#" title="\nin">&notin;</a>
		<a  onclick="f1(this.title)" href="#" title="\subset">&sub;</a>
		<a  onclick="f1(this.title)" href="#" title="\supset">&sup;</a>
		<a  onclick="f1(this.title)" href="#" title="\subseteq">&sube;</a>
		<a  onclick="f1(this.title)" href="#" title="\supseteq">&supe;</a>
		<a  onclick="f1(this.title)" href="#" title="\setminus">\</a>
		<a  onclick="f1(this.title)" href="#" title="\cap">&cap;</a>
		<a  onclick="f1(this.title)" href="#" title="\cup">&cup;</a>
		<a  onclick="f1(this.title)" href="#" title="\wedge">&and;</a>
		<a  onclick="f1(this.title)" href="#" title="\vee">&or;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\forall">&forall;</a>
		<a  onclick="f1(this.title)" href="#" title="\exists">&exist;</a>
		<a  onclick="f1(this.title)" href="#" title="\Re">&real;</a>
		<a  onclick="f1(this.title)" href="#" title="\Im">&image;</a>
	      </div></div>
	    </div>
	    
	    <div class="pane">
	      <div class="header"><a href="#">Punctuation and Accents</a></div>
	      <div class="contents"><div class="padding">
		<a  onclick="f1(this.title)" href="#" title="\ldots">&hellip;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\leftarrow">&larr;</a>
		<a  onclick="f1(this.title)" href="#" title="\rightarrow">&rarr;</a>
		<a  onclick="f1(this.title)" href="#" title="\Leftarrow">&lArr;</a>
		<a  onclick="f1(this.title)" href="#" title="\Rightarrow">&rArr;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\left(">(</a>
		<a  onclick="f1(this.title)" href="#" title="\right)">)</a>
		<a  onclick="f1(this.title)" href="#" title="\left[">[</a>
		<a  onclick="f1(this.title)" href="#" title="\right]">]</a>
		<a  onclick="f1(this.title)" href="#" title="\left\{">{</a>
		<a  onclick="f1(this.title)" href="#" title="\right\}">}</a>
		<a  onclick="f1(this.title)" href="#" title="\left\langle">&lang;</a>
		<a  onclick="f1(this.title)" href="#" title="\right\rangle">&rang;</a>
		<a  onclick="f1(this.title)" href="#" title="\|">||</a>
		<a  onclick="f1(this.title)" href="#" title="\lceil">&lceil;</a>
		<a  onclick="f1(this.title)" href="#" title="\rceil">&rceil;</a>
		<a  onclick="f1(this.title)" href="#" title="\lfloor">&lfloor;</a>
		<a  onclick="f1(this.title)" href="#" title="\rfloor">&rfloor;</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="'"><img src="../IMAGES/prime.png" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\hat{}"><img src="../IMAGES/hat.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\tilde{}"><img src="../IMAGES/tilde.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\bar{}"><img src="../IMAGES/bar.png" style="padding-top:4px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\vec{}"><img src="../IMAGES/vec.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\dot{}"><img src="../IMAGES/dot.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\ddot{}"><img src="../IMAGES/ddot.png" style="padding-top:2px;" alt="" /></a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\`{}"><img src="../IMAGES/grave.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\'{}"><img src="../IMAGES/acute.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\^{}"><img src="../IMAGES/circ.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\&quot;{}"><img src="../IMAGES/uml.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\~{}"><img src="../IMAGES/ttilde.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\r{}"><img src="../IMAGES/ring.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\u{}"><img src="../IMAGES/breve.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\v{}"><img src="../IMAGES/hachek.png" style="padding-top:2px;" alt="" /></a>
		<a  onclick="f1(this.title)" href="#" title="\c{}"><img src="../IMAGES/cedilla.png" style="padding-top:2px;" alt="" /></a>
	      </div></div>
	    </div>
	    
	    <div class="pane">
	      <div class="header"><a href="#">Functions</a></div>
	      <div class="contents" style="font-size: 80%"><div class="padding">
		<a  onclick="f1(this.title)" href="#" title="\arccos">arccos</a>
		<a  onclick="f1(this.title)" href="#" title="\arcsin">arcsin</a>
		<a  onclick="f1(this.title)" href="#" title="\arctan">arctan</a>
		<a  onclick="f1(this.title)" href="#" title="\cos">cos</a>
		<a  onclick="f1(this.title)" href="#" title="\cosh">cosh</a>
		<a  onclick="f1(this.title)" href="#" title="\cot">cot</a>
		<a  onclick="f1(this.title)" href="#" title="\coth">coth</a>
		<a  onclick="f1(this.title)" href="#" title="\csc">csc</a>
		<a  onclick="f1(this.title)" href="#" title="\sec">sec</a>
		<a  onclick="f1(this.title)" href="#" title="\sin">sin</a>
		<a  onclick="f1(this.title)" href="#" title="\sinh">sinh</a>
		<a  onclick="f1(this.title)" href="#" title="\tan">tan</a>
		<a  onclick="f1(this.title)" href="#" title="\tanh">tanh</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\exp">exp</a>
		<a  onclick="f1(this.title)" href="#" title="\log">log</a>
		<a  onclick="f1(this.title)" href="#" title="\ln">ln</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\max">max</a>
		<a  onclick="f1(this.title)" href="#" title="\min">min</a>
		<a  onclick="f1(this.title)" href="#" title="\sup">sup</a>
		<a  onclick="f1(this.title)" href="#" title="\inf">inf</a>
		<hr />
		<a  onclick="f1(this.title)" href="#" title="\lim">lim</a>
		<a  onclick="f1(this.title)" href="#" title="\gcd">gcd</a>
		<a  onclick="f1(this.title)" href="#" title="\hom">hom</a>
		<a  onclick="f1(this.title)" href="#" title="\ker">ker</a>
		<a  onclick="f1(this.title)" href="#" title="\det">det</a>
		<a  onclick="f1(this.title)" href="#" title="\bmod">mod</a>		
	      </div></div>
	    </div>
	    </div>
	 
	 </asp:Panel> 
</td>

 </tr>
 <tr>
 <td  style="width:50%;">
 <input type="button"  value ="Reset" onclick ="funcReset();" style="background-color:Gray; font-weight:bold;"/>
 </td>
 
 </tr>
 <tr > 
 <td style="width:100%;font-family:Arial; font-size:small;" colspan ="3" > 
 NOTE :- <br />
 1)In Math Editor, Here create Mathematical equations.<br />
 2)From Layout Window,Click on image you want to use to create Equations.<br />
 3)Paste the image on Rich text box for creating Questions.<br />
 4)Click on Reset button to clear text.
 
 </td>
 
 </tr>
 </table> 

 
<%--   <input type ="button"  onkeyup ="getimage();" runat="server"/>--%>
 </div>    
    
    
    <%-- <div id="images" style="vertical-align:top; float:right;">
        --%>
        
    
         
     <%--   </div>--%>
         
         
      </div>
     
         <script type ="text/javascript">

             function funcReset() {
                 document.getElementById("getdata").innerHTML="";
                 document.getElementById("latex").value = "";
             }


         $(document).ready(function() {
             $('#latexbuttons').accordion({ header: "div.header", autoHeight: false, collapsible: true, active: false });

         });
             

        $('#latexbuttons .header').click(function(e) { $('#latex').focus(); });

            
            
             function f1(mis) {
                 //alert(mis);
                 document.getElementById("latex").value += mis;
             
             }
         
         
         
         </script>
        
  </form>     
  <%--
    <input type ="text" value="$$3^{2}$$" />--%>
<%-- <img src="$$a^{b}$$" />--%>

 
    </body>
  

    
</html>
