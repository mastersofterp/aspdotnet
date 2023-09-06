﻿<!DOCTYPE html>
<html xml:lang="en" lang="en" dir="ltr">
<head>

    <title>Math Equation</title>
    <meta name="robots" content="index, nofollow" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-language" content="EN" />
    <link href="../plugins/newbootstrap/css/equation.css" rel="stylesheet" />

    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />


    <link href="<%=Page.ResolveClientUrl("~/plugins/select2/select2.css")%>" rel="stylesheet" />


    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>


    <script src="<%=Page.ResolveClientUrl("~/plugins/select2/select2.js")%>"></script>

    <script type="text/javascript">AC_FL_RunContent = 0;</script>
    <script src="../plugins/newbootstrap/js/eqconfig.js"></script>
    <script src="../plugins/newbootstrap/js/editorcombo.js"></script>

    <script type="text/javascript" src="JQuery/eqconfig.js"></script>
    <script type="text/javascript" src="JQuery/editorcombo.js"></script>

    <script type="text/javascript">
        EqnExport = function (text) {
            EqEditor.addText(window.opener.document, '', text);
            window.blur();
        };
        window.onresize = function () { EqEditor.setAdvert(); };

        google_ad_client = "ca-pub-2780686041799472";
        google_ad_slot = "0356195497";
        google_ad_width = 160;
        google_ad_height = 600;
    </script>
</head>
<body onload="var a=new EqTextArea(''); a.addExportArea('eqcoderaw','htmledit'); EqEditor.init('1587547898', a,true); EqEditor.ExportButton.add(a, 'copybutton', EqnExport,'htmledit'); EqEditor.load(''); EqEditor.setAdvert(); ">
    <div id="wrap" class="wrap">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">Math Editor</h3>
                    </div>

                    <div class="box-body">

                        <div id="hover"></div>

                        <div id="bar1" class="col-md-12 col-sm-12 col-12">
                            <div class="toolbar_wrapper">
                                <div class="toolbar" style="z-index: 23">
                                    <div class="panel">

                                        <%-- <img id="undobutton" src="../IMAGES/undo-x.gif" alt="undo" title="undo" />
                        <img id="redobutton" src="../IMAGES/redo-x.gif" alt="redo" title="redo" />--%>
                                        <input type="button" class="btn btn-primary" onclick="EqEditor.clearText()" value="Clear" title="Clear the editor window" />
                                    </div>
                                    <div class="panel">
                                        <select title="Equation Color" class="form-control" data-select2-enable="true" onchange="EqEditor.insert(this.value, this.value.length-1); this.selectedIndex=0">
                                            <option value="">Colors...</option>
                                            <option value="{\color{Red} }" style="color: Red">Red</option>
                                            <option value="{\color{Green} }" style="color: Green">Green</option>
                                            <option value="{\color{Blue} }" style="color: Blue">Blue</option>
                                            <option value="{\color{Yellow} }" style="color: Yellow">Yellow</option>
                                            <option value="{\color{Cyan} }" style="color: Cyan">Cyan</option>
                                            <option value="{\color{Magenta} }" style="color: Magenta">Magenta</option>
                                            <option value="{\color{Teal} }" style="color: Teal">Teal</option>
                                            <option value="{\color{Purple} }" style="color: Purple">Purple</option>
                                            <option value="{\color{DarkBlue} }" style="color: DarkBlue">Dark Blue</option>
                                            <option value="{\color{DarkRed} }" style="color: DarkRed">Dark Red</option>
                                            <option value="{\color{Orange} }" style="color: Orange">Orange</option>
                                            <option value="{\color{DarkOrange} }" style="color: DarkOrange">Dark Orange</option>
                                            <option value="{\color{Golden} }" style="color: Golden">Golden</option>
                                            <option value="{\color{Pink} }" style="color: Pink">Pink</option>
                                            <option value="{\color{DarkGreen} }" style="color: DarkGreen">Dark Green</option>
                                            <option value="{\color{Orchid} }" style="color: Orchid">Orchid</option>
                                            <option value="{\color{Emerald} }" style="color: Emerald">Emerald</option>
                                        </select>
                                    </div>
                                    <div class="panel">
                                        <select title="Functions" class="form-control" data-select2-enable="true" onchange="EqEditor.insert(this.value); this.selectedIndex=0;">
                                            <option selected="selected" value="" style="color: #8080ff">Functions&hellip;</option>
                                            <option value="\displaystyle">display style</option>
                                            <optgroup label="Trig">
                                                <option value="\sin">sin</option>
                                                <option value="\cos">cos</option>
                                                <option value="\tan">tan</option>
                                                <option value="\csc">csc</option>
                                                <option value="\sec">sec</option>
                                                <option value="\cot">cot</option>
                                                <option value="\sinh">sinh</option>
                                                <option value="\cosh">cosh</option>
                                                <option value="\tanh">tanh</option>
                                                <option value="\coth">coth</option>
                                            </optgroup>
                                            <optgroup label="Inverse Trig">
                                                <option value="\arcsin">arcsin</option>
                                                <option value="\arccos">arccos</option>
                                                <option value="\arctan">arctan</option>
                                                <option value="\textrm{arccsc}">arccsc</option>
                                                <option value="\textrm{arcsec}">arcsec</option>
                                                <option value="\textrm{arccot}">arccot</option>
                                                <option value="\sin^{-1}">sin-1</option>
                                                <option value="\cos^{-1}">cos-1</option>
                                                <option value="\tan^{-1}">tan-1</option>
                                                <option value="\sinh^{-1}">sinh-1</option>
                                                <option value="\cosh^{-1}">cosh-1</option>
                                                <option value="\tanh^{-1}">tanh-1</option>
                                            </optgroup>
                                            <optgroup label="Logs">
                                                <option value="\exp">exp</option>
                                                <option value="\lg">lg</option>
                                                <option value="\ln">ln</option>
                                                <option value="\log">log</option>
                                                <option value="\log_{e}">log e</option>
                                                <option value="\log_{10}">log 10</option>
                                            </optgroup>
                                            <optgroup label="Limits">
                                                <option value="\lim">limit</option>
                                                <option value="\liminf">liminf</option>
                                                <option value="\limsup">limsup</option>
                                                <option value="\max">maximum</option>
                                                <option value="\min">minimum</option>
                                                <option value="\infty">infinite</option>
                                            </optgroup>
                                            <optgroup label="Operators">
                                                <option value="\arg">arg</option>
                                                <option value="\det">det</option>
                                                <option value="\dim">dim</option>
                                                <option value="\gcd">gcd</option>
                                                <option value="\hom">hom</option>
                                                <option value="\ker">ker</option>
                                                <option value="\Pr">Pr</option>
                                                <option value="\sup">sup</option>
                                            </optgroup>
                                        </select>
                                    </div>
                                    <div class="panel">
                                        <input type="button" class="btn btn-primary" value="Examples" title="Common equations for different subjects" onclick="EqEditor.Example.show(this, 'algebra');" />
                                    </div>
                                    <div class="panel">
                                        <input type="button" class="btn btn-primary" value="History" title="A history of recently entered equations" onclick="    EqEditor.Example.show(this, 'history');" />
                                    </div>
                                    <div class="panel">
                                        <a href="http://en.wikipedia.org/wiki/Help:Formula" target="_blank">
                                            <img src="../IMAGES/i.gif" alt="help" title="help" width="13" height="13" border="0" /></a>

                                    </div>

                                </div>

                            </div>
                            <div class="toolbar_wrapper">
                                <div class="toolbar" style="z-index: 23">
                                    <div class="panel" id="panel14" style="height: 23px">
                                        <img src="../IMAGES/style.gif" width="106" height="184" border="0" title="Style" alt="Style Panel" usemap="#style_map" />
                                        <map name="style_map" id="style_map">
                                            <area shape="rect" alt="\boldsymbol{\alpha\beta\gamma123}" title="Math Bold Greek" coords="0,0,50,20" onclick="EqEditor.insert('\\boldsymbol{}')" />
                                            <area shape="rect" alt="\mathbf{Abc123}" title="Math Bold" coords="0,23,50,43" onclick="EqEditor.insert('\\mathbf{}')" />
                                            <area shape="rect" alt="\mathit{Abc123}" title="Math Italic" coords="0,46,50,66" onclick="EqEditor.insert('\\mathit{}')" />
                                            <area shape="rect" alt="\mathrm{Abc123}" title="Math Roman" coords="0,69,50,89" onclick="EqEditor.insert('\\mathrm{}')" />
                                            <area shape="rect" alt="\mathfrak{Abc123}" title="Math Fraktur" coords="0,92,50,112" onclick="EqEditor.insert('\\mathfrak{}')" />
                                            <area shape="rect" alt="\mathbb{Abc123}" title="Math Blackboard" coords="0,115,50,135" onclick="EqEditor.insert('\\mathbb{}')" />
                                            <area shape="rect" alt="\textup{Abc 123}" title="Text Upright" coords="53,0,103,20" onclick="EqEditor.insert('\\textup{}')" />
                                            <area shape="rect" alt="\textbf{Abc 123}" title="Text Bold" coords="53,23,103,43" onclick="EqEditor.insert('\\textbf{}')" />
                                            <area shape="rect" alt="\textit{Abc 123}" title="Text Italic" coords="53,46,103,66" onclick="EqEditor.insert('\\textit{}')" />
                                            <area shape="rect" alt="\textrm{Abc 123}" title="Text Roman" coords="53,69,103,89" onclick="EqEditor.insert('\\textrm{}')" />
                                            <area shape="rect" alt="\textsl{Abc 123}" title="Text Slanted" coords="53,92,103,112" onclick="EqEditor.insert('\\textsl{}')" />
                                            <area shape="rect" alt="\texttt{Abc 123}" title="Text Typewriter" coords="53,115,103,135" onclick="EqEditor.insert('\\texttt{}')" />
                                            <area shape="rect" alt="\textsc{Abc 123}" title="Text Small Caps" coords="53,138,103,158" onclick="EqEditor.insert('\\textsc{}')" />
                                            <area shape="rect" alt="\emph{Abc 123}" title="Text Emphasis" coords="53,161,103,181" onclick="EqEditor.insert('\\emph{}')" />
                                        </map>
                                    </div>
                                    <div class="panel" id="panel13" style="height: 34px">
                                        <img src="../IMAGES/spaces.gif" width="31" height="68" border="0" title="Spaces" alt="Spaces Panel" usemap="#spaces_map" />
                                        <map name="spaces_map" id="spaces_map">
                                            <area shape="rect" alt="\square\underline{\,}\square" title="thin space" coords="0,0,28,14" onclick="EqEditor.insert('\\,')" />
                                            <area shape="rect" alt="\square\underline{\:}\square" title="medium space" coords="0,17,28,31" onclick="EqEditor.insert('\\:')" />
                                            <area shape="rect" alt="\square\underline{\;}\square" title="thick space" coords="0,34,28,48" onclick="EqEditor.insert('\\;')" />
                                            <area shape="rect" alt="\square\!\square" title="negative space" coords="0,51,28,65" onclick="EqEditor.insert('\\!')" />
                                        </map>
                                    </div>
                                    <div class="panel" id="panel4" style="height: 34px">
                                        <img src="../IMAGES/binary.gif" width="68" height="238" border="0" title="Binary" alt="Binary Panel" usemap="#binary_map" /><map name="binary_map" id="binary_map"><area shape="rect" alt="\pm" coords="0,0,14,14" /><area shape="rect" alt="\mp" coords="0,17,14,31" /><area shape="rect" alt="\times" coords="0,34,14,48" /><area shape="rect" alt="\ast" coords="0,51,14,65" /><area shape="rect" alt="\div" coords="0,68,14,82" /><area shape="rect" alt="\setminus" coords="0,85,14,99" /><area shape="rect" alt="\dotplus" coords="0,102,14,116" /><area shape="rect" alt="\amalg" coords="0,119,14,133" /><area shape="rect" alt="\dagger" coords="0,136,14,150" /><area shape="rect" alt="\ddagger" coords="0,153,14,167" /><area shape="rect" alt="\wr" coords="0,170,14,184" /><area shape="rect" alt="\diamond" coords="0,187,14,201" /><area shape="rect" alt="\circledcirc" coords="0,204,14,218" /><area shape="rect" alt="\circledast" coords="0,221,14,235" /><area shape="rect" alt="\cap" coords="17,0,31,14" /><area shape="rect" alt="\Cap" coords="17,17,31,31" /><area shape="rect" alt="\sqcap" coords="17,34,31,48" /><area shape="rect" alt="\wedge" coords="17,51,31,65" /><area shape="rect" alt="\barwedge" coords="17,68,31,82" /><area shape="rect" alt="\triangleleft" coords="17,85,31,99" /><area shape="rect" alt="\lozenge" coords="17,102,31,116" /><area shape="rect" alt="\circ" coords="17,119,31,133" /><area shape="rect" alt="\square" coords="17,136,31,150" /><area shape="rect" alt="\triangle" coords="17,153,31,167" /><area shape="rect" alt="\triangledown" coords="17,170,31,184" /><area shape="rect" alt="\ominus" coords="17,187,31,201" /><area shape="rect" alt="\oslash" coords="17,204,31,218" /><area shape="rect" alt="\circleddash" coords="17,221,31,235" /><area shape="rect" alt="\cup" coords="34,0,48,14" /><area shape="rect" alt="\Cup" coords="34,17,48,31" /><area shape="rect" alt="\sqcup" coords="34,34,48,48" /><area shape="rect" alt="\vee" coords="34,51,48,65" /><area shape="rect" alt="\veebar" coords="34,68,48,82" /><area shape="rect" alt="\triangleright" coords="34,85,48,99" /><area shape="rect" alt="\blacklozenge" coords="34,102,48,116" /><area shape="rect" alt="\bullet" coords="34,119,48,133" /><area shape="rect" alt="\blacksquare" coords="34,136,48,150" /><area shape="rect" alt="\blacktriangle" coords="34,153,48,167" /><area shape="rect" alt="\blacktriangledown" coords="34,170,48,184" /><area shape="rect" alt="\oplus" coords="34,187,48,201" /><area shape="rect" alt="\otimes" coords="34,204,48,218" /><area shape="rect" alt="\odot" coords="34,221,48,235" /><area shape="rect" alt="\cdot" coords="51,0,65,14" /><area shape="rect" alt="\uplus" coords="51,17,65,31" /><area shape="rect" alt="\bigsqcup" coords="51,34,65,48" /><area shape="rect" alt="\bigtriangleup" coords="51,51,65,65" /><area shape="rect" alt="\bigtriangledown" coords="51,68,65,82" /><area shape="rect" alt="\star" coords="51,85,65,99" /><area shape="rect" alt="\bigstar" coords="51,102,65,116" /><area shape="rect" alt="\bigcirc" coords="51,119,65,133" /><area shape="rect" alt="\bigoplus" coords="51,136,65,150" /><area shape="rect" alt="\bigotimes" coords="51,153,65,167" /><area shape="rect" alt="\bigodot" coords="51,170,65,184" /></map>
                                    </div>
                                    <div class="panel" id="panel16" style="height: 34px">
                                        <img src="../IMAGES/symbols.gif" width="68" height="136" border="0" title="Symbols" alt="Symbols Panel" usemap="#symbols_map" /><map name="symbols_map" id="symbols_map"><area shape="rect" alt="\therefore" title="therefore" coords="0,0,14,14" /><area shape="rect" alt="\because" title="because" coords="0,17,14,31" /><area shape="rect" alt="\cdots" title="horizontal dots" coords="0,34,14,48" /><area shape="rect" alt="\ddots" title="diagonal dots" coords="0,51,14,65" /><area shape="rect" alt="\vdots" title="vertical dots" coords="0,68,14,82" /><area shape="rect" alt="\S" title="section" coords="0,85,14,99" /><area shape="rect" alt="\P" title="paragraph" coords="0,102,14,116" /><area shape="rect" alt="\copyright" title="copyright" coords="0,119,14,133" /><area shape="rect" alt="\partial" title="partial" coords="17,0,31,14" /><area shape="rect" alt="\imath" coords="17,17,31,31" /><area shape="rect" alt="\jmath" coords="17,34,31,48" /><area shape="rect" alt="\Re" title="real" coords="17,51,31,65" /><area shape="rect" alt="\Im" title="imaginary" coords="17,68,31,82" /><area shape="rect" alt="\forall" coords="17,85,31,99" /><area shape="rect" alt="\exists" coords="17,102,31,116" /><area shape="rect" alt="\top" coords="17,119,31,133" /><area shape="rect" alt="\mathbb{P}" title="prime" coords="34,0,48,14" /><area shape="rect" alt="\mathbb{N}" title="natural" coords="34,17,48,31" /><area shape="rect" alt="\mathbb{Z}" title="integers" coords="34,34,48,48" /><area shape="rect" alt="\mathbb{I}" title="irrational" coords="34,51,48,65" /><area shape="rect" alt="\mathbb{Q}" title="rational" coords="34,68,48,82" /><area shape="rect" alt="\mathbb{R}" title="real" coords="34,85,48,99" /><area shape="rect" alt="\mathbb{C}" title="complex" coords="34,102,48,116" /><area shape="rect" alt="\angle" coords="51,0,65,14" /><area shape="rect" alt="\measuredangle" coords="51,17,65,31" /><area shape="rect" alt="\sphericalangle" coords="51,34,65,48" /><area shape="rect" alt="\varnothing" coords="51,51,65,65" /><area shape="rect" alt="\infty" coords="51,68,65,82" /><area shape="rect" alt="\mho" coords="51,85,65,99" /><area shape="rect" alt="\wp" coords="51,102,65,116" /></map>
                                    </div>
                                    <div class="panel" id="panel6" style="height: 34px">
                                        <img src="../IMAGES/foreign.gif" width="34" height="136" border="0" title="Foreign" alt="Foreign Panel" usemap="#foreign_map" /><map name="foreign_map" id="foreign_map"><area shape="rect" alt="\aa" coords="0,0,14,14" /><area shape="rect" alt="\ae" coords="0,17,14,31" /><area shape="rect" alt="\l" coords="0,34,14,48" /><area shape="rect" alt="\o" coords="0,51,14,65" /><area shape="rect" alt="\oe" coords="0,68,14,82" /><area shape="rect" alt="\ss" coords="0,85,14,99" /><area shape="rect" alt="\$" title="Dollar" coords="0,102,14,116" /><area shape="rect" alt="\cent" title="Cent" coords="0,119,14,133" /><area shape="rect" alt="\AA" coords="17,0,31,14" /><area shape="rect" alt="\AE" coords="17,17,31,31" /><area shape="rect" alt="\L" coords="17,34,31,48" /><area shape="rect" alt="\O" coords="17,51,31,65" /><area shape="rect" alt="\OE" coords="17,68,31,82" /><area shape="rect" alt="\SS" coords="17,85,31,99" /><area shape="rect" alt="\pounds" title="Pound" coords="17,102,31,116" /><area shape="rect" alt="\euro" title="Euro" coords="17,119,31,133" /></map>
                                    </div>
                                    <div class="panel" id="panel15" style="height: 34px">
                                        <img src="../IMAGES/subsupset.gif" width="34" height="153" border="0" title="Subsupset" alt="Subsupset Panel" usemap="#subsupset_map" /><map name="subsupset_map" id="subsupset_map"><area shape="rect" alt="\sqsubset" coords="0,0,14,14" /><area shape="rect" alt="\sqsubseteq" coords="0,17,14,31" /><area shape="rect" alt="\subset" coords="0,34,14,48" /><area shape="rect" alt="\subseteq" coords="0,51,14,65" /><area shape="rect" alt="\nsubseteq" coords="0,68,14,82" /><area shape="rect" alt="\subseteqq" coords="0,85,14,99" /><area shape="rect" alt="\nsubseteq" coords="0,102,14,116" /><area shape="rect" alt="\in" coords="0,119,14,133" /><area shape="rect" alt="\notin" coords="0,136,14,150" /><area shape="rect" alt="\sqsupset" coords="17,0,31,14" /><area shape="rect" alt="\sqsupseteq" coords="17,17,31,31" /><area shape="rect" alt="\supset" coords="17,34,31,48" /><area shape="rect" alt="\supseteq" coords="17,51,31,65" /><area shape="rect" alt="\nsupseteq" coords="17,68,31,82" /><area shape="rect" alt="\supseteqq" coords="17,85,31,99" /><area shape="rect" alt="\nsupseteqq" coords="17,102,31,116" /><area shape="rect" alt="\ni" coords="17,119,31,133" /></map>
                                    </div>
                                    <div class="panel" id="panel1" style="height: 34px">
                                        <img src="../IMAGES/accents.gif" width="34" height="119" border="0" title="Accents" alt="Accents Panel" usemap="#accents_map" /><map name="accents_map" id="accents_map"><area shape="rect" alt="a'" coords="0,0,14,14" onclick="EqEditor.insert('{}\'')" /><area shape="rect" alt="\dot{a}" coords="0,17,14,31" onclick="EqEditor.insert('\\dot{}')" /><area shape="rect" alt="\hat{a}" coords="0,34,14,48" onclick="EqEditor.insert('\\hat{}')" /><area shape="rect" alt="\grave{a}" coords="0,51,14,65" onclick="EqEditor.insert('\\grave{}')" /><area shape="rect" alt="\tilde{a}" coords="0,68,14,82" onclick="EqEditor.insert('\\tilde{}')" /><area shape="rect" alt="\bar{a}" coords="0,85,14,99" onclick="EqEditor.insert('\\bar{}')" /><area shape="rect" alt="\not{a}" coords="0,102,14,116" /><area shape="rect" alt="a''" coords="17,0,31,14" onclick="EqEditor.insert('{}\'\'')" /><area shape="rect" alt="\ddot{a}" coords="17,17,31,31" onclick="EqEditor.insert('\\ddot{}')" /><area shape="rect" alt="\check{a}" coords="17,34,31,48" onclick="EqEditor.insert('\\check{}')" /><area shape="rect" alt="\acute{a}" coords="17,51,31,65" onclick="EqEditor.insert('\\acute{}')" /><area shape="rect" alt="\breve{a}" coords="17,68,31,82" onclick="EqEditor.insert('\\breve{}')" /><area shape="rect" alt="\vec{a}" coords="17,85,31,99" onclick="EqEditor.insert('\\vec{}')" /><area shape="rect" alt="a^{\circ}" title="degrees" coords="17,102,31,116" onclick="EqEditor.insert('^{\\circ}',0)" /></map>
                                    </div>
                                    <div class="panel" id="panel2" style="height: 34px">
                                        <img src="../IMAGES/accents_ext.gif" width="25" height="170" border="0" title="Accents_ext" alt="Accents_ext Panel" usemap="#accents_ext_map" /><map name="accents_ext_map" id="accents_ext_map"><area shape="rect" alt="\widetilde{abc}" coords="0,0,22,14" onclick="EqEditor.insert('\\widetilde{}',11)" /><area shape="rect" alt="\widehat{abc}" coords="0,17,22,31" onclick="EqEditor.insert('\\widehat{}',9)" /><area shape="rect" alt="\overleftarrow{abc}" coords="0,34,22,48" onclick="EqEditor.insert('\\overleftarrow{}',15)" /><area shape="rect" alt="\overrightarrow{abc}" coords="0,51,22,65" onclick="EqEditor.insert('\\overrightarrow{}',16)" /><area shape="rect" alt="\overline{abc}" coords="0,68,22,82" onclick="EqEditor.insert('\\overline{}',10)" /><area shape="rect" alt="\underline{abc}" coords="0,85,22,99" onclick="EqEditor.insert('\\underline{}',11)" /><area shape="rect" alt="\overbrace{abc}" coords="0,102,22,116" onclick="EqEditor.insert('\\overbrace{}',11)" /><area shape="rect" alt="\underbrace{abc}" coords="0,119,22,133" onclick="EqEditor.insert('\\underbrace{}',12)" /><area shape="rect" alt="\overset{a}{abc}" coords="0,136,22,150" onclick="EqEditor.insert('\\overset{}{}',9,11)" /><area shape="rect" alt="\underset{a}{abc}" coords="0,153,22,167" onclick="EqEditor.insert('\\underset{}{}',10,12)" /></map>
                                    </div>
                                    <div class="panel" id="panel3" style="height: 34px">
                                        <img src="../IMAGES/arrows.gif" width="56" height="170" border="0" title="Arrows" alt="Arrows Panel" usemap="#arrows_map" /><map name="arrows_map" id="arrows_map"><area shape="rect" alt="x \mapsto x^2" title="\mapsto" coords="0,0,25,14" /><area shape="rect" alt="\leftarrow" coords="0,17,25,31" /><area shape="rect" alt="\Leftarrow" coords="0,34,25,48" /><area shape="rect" alt="\leftrightarrow" coords="0,51,25,65" /><area shape="rect" alt="\leftharpoonup" coords="0,68,25,82" /><area shape="rect" alt="\leftharpoondown" coords="0,85,25,99" /><area shape="rect" alt="\leftrightharpoons" coords="0,102,25,116" /><area shape="rect" alt="\xleftarrow[text]{long}" coords="0,119,25,133" onclick="EqEditor.insert('\\xleftarrow[]{}',12)" /><area shape="rect" alt="\overset{a}{\leftarrow}" coords="0,136,25,150" onclick="EqEditor.insert('\\overset{}{\\leftarrow}',9)" /><area shape="rect" alt="\underset{a}{\leftarrow}" coords="0,153,25,167" onclick="EqEditor.insert('\\underset{}{\\leftarrow}',10)" /><area shape="rect" alt="n \to" coords="28,0,53,14" /><area shape="rect" alt="\rightarrow" coords="28,17,53,31" /><area shape="rect" alt="\Rightarrow" coords="28,34,53,48" /><area shape="rect" alt="\Leftrightarrow" coords="28,51,53,65" /><area shape="rect" alt="\rightharpoonup" coords="28,68,53,82" /><area shape="rect" alt="\rightharpoondown" coords="28,85,53,99" /><area shape="rect" alt="\rightleftharpoons" coords="28,102,53,116" /><area shape="rect" alt="\xrightarrow[text]{long}" coords="28,119,53,133" onclick="EqEditor.insert('\\xrightarrow[]{}',13)" /><area shape="rect" alt="\overset{a}{\rightarrow}" coords="28,136,53,150" onclick="EqEditor.insert('\\overset{}{\\rightarrow}',9)" /><area shape="rect" alt="\underset{a}{\rightarrow}" coords="28,153,53,167" onclick="EqEditor.insert('\\underset{}{\\rightarrow}',10)" /></map>
                                    </div>
                                </div>
                            </div>
                            <div class="toolbar_wrapper">
                                <div class="toolbar" style="z-index: 22">
                                    <div class="panel" id="panel11" style="height: 28px">
                                        <img src="../IMAGES/operators.gif" width="168" height="140" border="0" title="Operators" alt="Operators Panel" usemap="#operators_map" /><map name="operators_map" id="operators_map"><area shape="rect" alt="x^a" title="superscript" coords="0,0,25,25" onclick="EqEditor.insert('^{}',2,0)" /><area shape="rect" alt="x_a" title="subscript" coords="0,28,25,53" onclick="EqEditor.insert('_{}',2,0)" /><area shape="rect" alt="x_a^b" coords="0,56,25,81" onclick="EqEditor.insert('_{}^{}',2,0)" /><area shape="rect" alt="{x_a}^b" coords="0,84,25,109" onclick="EqEditor.insert('{_{}}^{}',1)" /><area shape="rect" alt="_a^{b}\textrm{C}" title="_{a}^{b}\textrm{C}" coords="0,112,25,137" onclick="EqEditor.insert('_{}^{}\\textrm{}',2,14)" /><area shape="rect" alt="\frac{a}{b}" title="fraction" coords="28,0,53,25" onclick="EqEditor.insert('\\frac{}{}',6)" /><area shape="rect" alt="x\tfrac{a}{b}" title="tiny fraction" coords="28,28,53,53" onclick="EqEditor.insert('\\tfrac{}{}',7)" /><area shape="rect" alt="\frac{\partial }{\partial x}" coords="28,56,53,81" onclick="EqEditor.insert('\\frac{\\partial }{\\partial x}',15)" /><area shape="rect" alt="\frac{\partial^2 }{\partial x^2}" coords="28,84,53,109" onclick="EqEditor.insert('\\frac{\\partial^2 }{\\partial x^2}',17)" /><area shape="rect" alt="\frac{\mathrm{d} }{\mathrm{d} x}" coords="28,112,53,137" onclick="EqEditor.insert('\\frac{\\mathrm{d} }{\\mathrm{d} x}',17)" /><area shape="rect" alt="\int" coords="56,0,81,25" /><area shape="rect" alt="\int_a^b" title="\int_{}^{}" coords="56,28,81,53" onclick="EqEditor.insert('\\int_{}^{}',6,1000)" /><area shape="rect" alt="\oint" coords="56,56,81,81" onclick="EqEditor.insert('\\oint')" /><area shape="rect" alt="\oint_a^b" title="\oint_{}^{}" coords="56,84,81,109" onclick="EqEditor.insert('\\oint_{}^{}',7,1000)" /><area shape="rect" alt="\iint_a^b" title="\iint_{}^{}" coords="56,112,81,137" onclick="EqEditor.insert('\\iint_{}^{}',7,1000)" /><area shape="rect" alt="\bigcap" coords="84,0,109,25" /><area shape="rect" alt="\bigcap_a^b" title="\bigcap_{}^{}" coords="84,28,109,53" onclick="EqEditor.insert('\\bigcap_{}^{}',9,1000)" /><area shape="rect" alt="\bigcup" coords="84,56,109,81" onclick="EqEditor.insert('\\bigcup')" /><area shape="rect" alt="\bigcup_a^b" title="\bigcup_{}^{}" coords="84,84,109,109" onclick="EqEditor.insert('\\bigcup_{}^{}',9,1000)" /><area shape="rect" alt="\displaystyle \lim_{x \to 0}" title="\lim_{x \to 0}" coords="84,112,109,137" onclick="EqEditor.insert('\\lim_{}')" /><area shape="rect" alt="\sum" coords="112,0,137,25" /><area shape="rect" alt="\sum_a^b" title="\sum_{}^{}" coords="112,28,137,53" onclick="EqEditor.insert('\\sum_{}^{}',6)" /><area shape="rect" alt="\sqrt{x}" title="\sqrt{}" coords="112,56,137,81" onclick="EqEditor.insert('\\sqrt{}',6,6)" /><area shape="rect" alt="\sqrt[n]{x}" title="\sqrt[]{}" coords="112,84,137,109" onclick="EqEditor.insert('\\sqrt[]{}',6,8)" /><area shape="rect" alt="\prod" coords="140,0,165,25" /><area shape="rect" alt="\prod_a^b" title="\prod_{}^{}" coords="140,28,165,53" onclick="EqEditor.insert('\\prod_{}^{}',7,1000)" /><area shape="rect" alt="\coprod" coords="140,56,165,81" /><area shape="rect" alt="\coprod_a^b" title="\coprod_{}^{}" coords="140,84,165,109" onclick="EqEditor.insert('\\coprod_{}^{}',9,1000)" /></map>
                                    </div>
                                    <div class="panel" id="panel5" style="height: 28px">
                                        <img src="../IMAGES/brackets.gif" width="56" height="140" border="0" title="Brackets" alt="Brackets Panel" usemap="#brackets_map" /><map name="brackets_map" id="brackets_map"><area shape="rect" alt="\left (\: \right )" title="\left ( \right )" coords="0,0,25,25" onclick="EqEditor.insert('\\left (  \\right )',8)" /><area shape="rect" alt="\left [\: \right ]" title="\left ( \right )" coords="0,28,25,53" onclick="EqEditor.insert('\\left [  \\right ]',8)" /><area shape="rect" alt="\left\{\: \right\}" title="\left\{ \right\}" coords="0,56,25,81" onclick="EqEditor.insert('\\left \\{  \\right \\}',9)" /><area shape="rect" alt="\left |\: \right |" title="\left | \right |" coords="0,84,25,109" onclick="EqEditor.insert('\\left |  \\right |',8)" /><area shape="rect" alt="\left \{ \cdots \right." title="\left \{ \right." coords="0,112,25,137" onclick="EqEditor.insert('\\left \\{  \\right.',9)" /><area shape="rect" alt="\left \|\: \right \|" title="\left \| \right \|" coords="28,0,53,25" onclick="EqEditor.insert('\\left \\|  \\right \\|',9)" /><area shape="rect" alt="\left \langle \: \right \rangle" title="\left \langle \right \rangle" coords="28,28,53,53" onclick="EqEditor.insert('\\left \\langle  \\right \\rangle',14)" /><area shape="rect" alt="\left \lfloor \: \right \rfloor" title="\left \lfloor \right \rfloor" coords="28,56,53,81" onclick="EqEditor.insert('\\left \\lfloor  \\right \\rfloor',14)" /><area shape="rect" alt="\left \lceil \: \right \rceil" title="\left \lceil \right \rceil" coords="28,84,53,109" onclick="EqEditor.insert('\\left \\lceil  \\right \\rceil',13)" /><area shape="rect" alt="\left. \cdots \right \}" title="\left. \right \}" coords="28,112,53,137" onclick="EqEditor.insert('\\left.  \\right \\}',7)" /></map>
                                    </div>
                                    <div class="panel" id="panel8" style="height: 34px">
                                        <img src="../IMAGES/greeklower.gif" width="68" height="136" border="0" title="Greeklower" alt="Greeklower Panel" usemap="#greeklower_map" /><map name="greeklower_map" id="greeklower_map"><area shape="rect" alt="\alpha" coords="0,0,14,14" /><area shape="rect" alt="\epsilon" coords="0,17,14,31" /><area shape="rect" alt="\theta" coords="0,34,14,48" /><area shape="rect" alt="\lambda" coords="0,51,14,65" /><area shape="rect" alt="\pi" coords="0,68,14,82" /><area shape="rect" alt="\sigma" coords="0,85,14,99" /><area shape="rect" alt="\phi" coords="0,102,14,116" /><area shape="rect" alt="\omega" coords="0,119,14,133" /><area shape="rect" alt="\beta" coords="17,0,31,14" /><area shape="rect" alt="\varepsilon" coords="17,17,31,31" /><area shape="rect" alt="\vartheta" coords="17,34,31,48" /><area shape="rect" alt="\mu" coords="17,51,31,65" /><area shape="rect" alt="\varpi" coords="17,68,31,82" /><area shape="rect" alt="\varsigma" coords="17,85,31,99" /><area shape="rect" alt="\varphi" coords="17,102,31,116" /><area shape="rect" alt="\gamma" coords="34,0,48,14" /><area shape="rect" alt="\zeta" coords="34,17,48,31" /><area shape="rect" alt="\iota" coords="34,34,48,48" /><area shape="rect" alt="\nu" coords="34,51,48,65" /><area shape="rect" alt="\rho" coords="34,68,48,82" /><area shape="rect" alt="\tau" coords="34,85,48,99" /><area shape="rect" alt="\chi" coords="34,102,48,116" /><area shape="rect" alt="\delta" coords="51,0,65,14" /><area shape="rect" alt="\eta" coords="51,17,65,31" /><area shape="rect" alt="\kappa" coords="51,34,65,48" /><area shape="rect" alt="\xi" coords="51,51,65,65" /><area shape="rect" alt="\varrho" coords="51,68,65,82" /><area shape="rect" alt="\upsilon" coords="51,85,65,99" /><area shape="rect" alt="\psi" coords="51,102,65,116" /></map>
                                    </div>
                                    <div class="panel" id="panel9" style="height: 34px">
                                        <img src="../IMAGES/greekupper.gif" width="34" height="102" border="0" title="Greekupper" alt="Greekupper Panel" usemap="#greekupper_map" /><map name="greekupper_map" id="greekupper_map"><area shape="rect" alt="\Gamma" coords="0,0,14,14" /><area shape="rect" alt="\Theta" coords="0,17,14,31" /><area shape="rect" alt="\Xi" coords="0,34,14,48" /><area shape="rect" alt="\Sigma" coords="0,51,14,65" /><area shape="rect" alt="\Phi" coords="0,68,14,82" /><area shape="rect" alt="\Omega" coords="0,85,14,99" /><area shape="rect" alt="\Delta" coords="17,0,31,14" /><area shape="rect" alt="\Lambda" coords="17,17,31,31" /><area shape="rect" alt="\Pi" coords="17,34,31,48" /><area shape="rect" alt="\Upsilon" coords="17,51,31,65" /><area shape="rect" alt="\Psi" coords="17,68,31,82" /></map>
                                    </div>
                                    <div class="panel" id="panel12" style="height: 34px">
                                        <img src="../IMAGES/relations.gif" width="51" height="221" border="0" title="Relations" alt="Relations Panel" usemap="#relations_map" /><map name="relations_map" id="relations_map"><area shape="rect" alt="&lt;" coords="0,0,14,14" /><area shape="rect" alt="\leq" coords="0,17,14,31" /><area shape="rect" alt="\leqslant" coords="0,34,14,48" /><area shape="rect" alt="\nless" coords="0,51,14,65" /><area shape="rect" alt="\nleqslant" coords="0,68,14,82" /><area shape="rect" alt="\prec" coords="0,85,14,99" /><area shape="rect" alt="\preceq" coords="0,102,14,116" /><area shape="rect" alt="\ll" coords="0,119,14,133" /><area shape="rect" alt="\vdash" coords="0,136,14,150" /><area shape="rect" alt="\smile" title="smile" coords="0,153,14,167" /><area shape="rect" alt="\models" coords="0,170,14,184" /><area shape="rect" alt="\mid" coords="0,187,14,201" /><area shape="rect" alt="\bowtie" coords="0,204,14,218" /><area shape="rect" alt="&gt;" coords="17,0,31,14" /><area shape="rect" alt="\geq" coords="17,17,31,31" /><area shape="rect" alt="\geqslant" coords="17,34,31,48" /><area shape="rect" alt="\ngtr" coords="17,51,31,65" /><area shape="rect" alt="\ngeqslant" coords="17,68,31,82" /><area shape="rect" alt="\succ" coords="17,85,31,99" /><area shape="rect" alt="\succeq" coords="17,102,31,116" /><area shape="rect" alt="\gg" coords="17,119,31,133" /><area shape="rect" alt="\dashv" coords="17,136,31,150" /><area shape="rect" alt="\frown" title="frown" coords="17,153,31,167" /><area shape="rect" alt="\perp" coords="17,170,31,184" /><area shape="rect" alt="\parallel" title="parallel" coords="17,187,31,201" /><area shape="rect" alt="\Join" coords="17,204,31,218" /><area shape="rect" alt="=" coords="34,0,48,14" /><area shape="rect" alt="\doteq" coords="34,17,48,31" /><area shape="rect" alt="\equiv" title="equivalent" coords="34,34,48,48" /><area shape="rect" alt="\neq" coords="34,51,48,65" /><area shape="rect" alt="\not\equiv" title="not equivalent" coords="34,68,48,82" /><area shape="rect" alt="\overset{\underset{\mathrm{def}}{}}{=}" title="define" coords="34,85,48,99" /><area shape="rect" alt="\sim" coords="34,102,48,116" /><area shape="rect" alt="\approx" coords="34,119,48,133" /><area shape="rect" alt="\simeq" coords="34,136,48,150" /><area shape="rect" alt="\cong" coords="34,153,48,167" /><area shape="rect" alt="\asymp" coords="34,170,48,184" /><area shape="rect" alt="\propto" title="proportional to" coords="34,187,48,201" /></map>
                                    </div>
                                    <div class="panel" id="panel10" style="height: 34px">
                                        <img src="../IMAGES/matrix.gif" width="102" height="170" border="0" title="Matrix" alt="Matrix Panel" usemap="#matrix_map" /><map name="matrix_map" id="matrix_map"><area shape="rect" alt="\begin{matrix}
\cdots \\
\cdots \\
\end{matrix}"
                                            title="\begin{matrix} ... \end{matrix}" coords="0,0,31,31" onclick="EqEditor.makeArrayMatrix('','','')" /><area shape="rect" alt="\begin{pmatrix}
\cdots \\
\cdots
\end{pmatrix}"
                                                title="\begin{pmatrix} ... \end{pmatrix}" coords="0,34,31,65" onclick="EqEditor.makeArrayMatrix('p','','')" /><area shape="rect" alt="\begin{vmatrix}
\cdots \\
\cdots
\end{vmatrix}"
                                                    title="\begin{vmatrix} ... \end{vmatrix}" coords="0,68,31,99" onclick="EqEditor.makeArrayMatrix('v','','')" /><area shape="rect" alt="\begin{Vmatrix}
\cdots \\ 
\cdots
\end{Vmatrix}"
                                                        title="\begin{Vmatrix} ... \end{Vmatrix}" coords="0,102,31,133" onclick="EqEditor.makeArrayMatrix('V','','')" /><area shape="rect" alt="\left.\begin{matrix}
\cdots \\ 
\cdots
\end{matrix}\right|"
                                                            title="\left.\begin{matrix}... \end{matrix}\right|" coords="0,136,31,167" onclick="EqEditor.makeArrayMatrix('','\\left.','\\right|')" /><area shape="rect" alt="\begin{bmatrix}
\cdots \\ 
\cdots
\end{bmatrix}"
                                                                title="\being{bmatrix} ... \end{bmatrix}" coords="34,0,65,31" onclick="EqEditor.makeArrayMatrix('b','','')" /><area shape="rect" alt="\bigl(\begin{smallmatrix}
\cdots \\ 
\cdots 
\end{smallmatrix}\bigr)"
                                                                    title="\bigl(\begin{smallmatrix} ... \end{smallmatrix}\bigr)" coords="34,34,65,65" onclick="EqEditor.makeArrayMatrix('small','\\bigl(','\\bigr)')" /><area shape="rect" alt="\begin{Bmatrix}
\cdots \\ 
\cdots
\end{Bmatrix}"
                                                                        title="\begin{Bmatrix} ... \end{Bmatrix}" coords="34,68,65,99" onclick="EqEditor.makeArrayMatrix('B','','')" /><area shape="rect" alt="\left\{\begin{matrix}
\cdots \\ 
\cdots
\end{matrix}\right."
                                                                            title="\begin{Bmatrix} ... \end{matrix}" coords="34,102,65,133" onclick="EqEditor.makeArrayMatrix('','\\left\\{','\\right.')" /><area shape="rect" alt="\left.\begin{matrix}
\cdots \\ 
\cdots
\end{matrix}\right\}"
                                                                                title="\begin{matrix} ... \end{Bmatrix}" coords="34,136,65,167" onclick="EqEditor.makeArrayMatrix('','\\left.','\\right\\}')" />
                                            <area shape="rect" alt=" \binom{n}{r}" coords="68,0,99,31" onclick="EqEditor.insert('\\binom{}{}')" />
                                            <area shape="rect" alt="\begin{cases}..,x= \\..,x=\end{cases}" title="\begin{cases} ... \end{cases}" coords="68,34,99,65" onclick="EqEditor.makeEquationsMatrix('cases', true, true)" />
                                            <area shape="rect" alt="\begin{align*}
y&amp;=\cdots \\ 
 &amp;+\cdots 
\end{align*}"
                                                title="\begin{align} ... \end{align}" coords="68,68,99,99" onclick="EqEditor.makeEquationsMatrix('align', false)" />
                                        </map>
                                    </div>
                                </div>
                            </div>
                            <div id="latex_copy">
                                <img src="../IMAGES/copy.gif" height="16" width="18" alt="copy" title="Copy to Clipboard" />
                            </div>
                        </div>

                        <div id="bar2" class="top" style="display: none">
                            <div style="position: absolute; right: 25px; z-index: 1000">
                                <img style="float: right" src="../IMAGES/cancel.gif" onclick="EqEditor.Example.hide();" alt="close" title="close panel" />
                            </div>
                            <div id="photos" style="position: relative; overflow: hidden; height: 68px; width: 600px; margin: 0 auto"></div>
                            <div style="height: 14px; width: 300px; margin: 0 auto">
                                <div style="float: left; margin-right: 5px">
                                    <img id="leftarrow" src="../IMAGES/leftarrow_grey.gif" height="14" width="14" alt="&lt;&lt;" title="previous page" onclick="EqnEditor.Gallery.right()" />
                                </div>
                                <div id="overview" style="float: left; width: 260px;"></div>
                                <div style="float: left; margin-left: 5px">
                                    <img id="rightarrow" src="../IMAGES/rightarrow_grey.gif" height="14" width="14" alt="&gt;&gt;" title="next page" onclick="EqnEditor.Gallery.left()" />
                                </div>
                            </div>
                            <div id="toolbar_example">
                                <input type="button" class="lightbluebutton" value="Algebra" onclick="EqEditor.Example.show(this, 'algebra');" />
                                <input type="button" class="lightbluebutton" value="Calculus" onclick="EqEditor.Example.show(this, 'calculus');" />
                                <input type="button" class="lightbluebutton" value="Stats" onclick="EqEditor.Example.show(this, 'stats');" />
                                <input type="button" class="lightbluebutton" value="Matrices" onclick="EqEditor.Example.show(this, 'matrices');" />
                                <input type="button" class="lightbluebutton" value="Sets" onclick="EqEditor.Example.show(this, 'sets');" />
                                <input type="button" class="lightbluebutton" value="Trig" onclick="EqEditor.Example.show(this, 'trig');" />
                                <input type="button" class="lightbluebutton" value="Geometry" onclick="EqEditor.Example.show(this, 'geometry');" />
                                <input type="button" class="lightbluebutton" value="Chemistry" onclick="EqEditor.Example.show(this, 'chemistry');" />
                                <input type="button" class="lightbluebutton" value="Physics" onclick="EqEditor.Example.show(this, 'physics');" />
                            </div>
                        </div>

                        <form>
                            <div id="input">
                                <div id="intro">
                                    <br />
                                    <strong>Type your equation in this box</strong>
                                </div>
                                <textarea name="latex_formula" id="latex_formula" cols="80" rows="8" spellcheck="false"></textarea>
                                <div class="toolbar_wrapper">
                                    <div class="toolbar" style="z-index: 11">
                                        <div class="panel">
                                            <select id="format" name="format" class="form-control" data-select2-enable="true" title="Select the output format">
                                                <option value="gif">gif</option>
                                                <option value="png">png</option>
                                                <option value="pdf">pdf</option>
                                                <option value="swf">swf</option>
                                                <option value="emf">emf</option>
                                                <option value="svg">svg</option>
                                            </select>
                                        </div>
                                        <div class="panel">
                                            <select id="font" name="font" class="form-control" data-select2-enable="true" title="Character Font">
                                                <option value="">Latin Modern</option>
                                                <option value="jvn">Verdana</option>
                                                <option value="cs">Comic Sans</option>
                                                <option value="cm">Computer Modern</option>
                                                <option value="phv">Helvetica</option>
                                            </select>
                                        </div>
                                        <div class="panel">
                                            <select name="fontsize" id="fontsize" class="form-control" data-select2-enable="true" title="Equation Font Size">
                                                <option value="\tiny">(5pt) Tiny</option>
                                                <option value="\small">(9pt) Small</option>
                                                <option value="" selected="selected">(10pt) Normal</option>
                                                <option value="\large">(12pt) Large</option>
                                                <option value="\LARGE">(18pt) Very Large</option>
                                                <option value="\huge">(20pt) Huge</option>
                                            </select>
                                        </div>
                                        <div class="panel">
                                            <select id="dpi" name="dpi" class="form-control" data-select2-enable="true" title="Select the output resolution">
                                                <option value="50">50</option>
                                                <option value="80">80</option>
                                                <option value="100">100</option>
                                                <option value="110" selected="selected">110</option>
                                                <option value="120">120</option>
                                                <option value="150">150</option>
                                                <option value="200">200</option>
                                                <option value="300">300</option>
                                            </select>
                                        </div>
                                        <div class="panel">
                                            <select id="bg" name="bg" class="form-control" data-select2-enable="true" title="Background color">
                                                <option value="Transparent">Transparent</option>
                                                <option value="White">White</option>
                                                <option value="Black">Black</option>
                                                <option value="Red">Red</option>
                                                <option value="Green">Green</option>
                                                <option value="Blue">Blue</option>
                                            </select>
                                        </div>
                                        <div class="panel mt-1">
                                            <input type="checkbox" id="inline" name="inline" title="Place equations inline with other text" />
                                            <label for="inline">Inline</label>&nbsp;<input type="checkbox" id="compressed" name="compressed" title="Create equations that are vertically compressed, suitable for being inline with other text" />
                                            <label for="compressed">Compressed</label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="preview">
                                <img id="equationview" name="equationview" title="This is the rendered form of the equation. You can not edit this directly. Right click will give you the option to save the image, and in most browsers you can drag the image onto your desktop or another program." src="../Images/spacer.gif" />
                                <div id="equationcomment">
                                </div>

                                <%--<div id="download"></div>--%>
                            </div>
                        </form>

                    </div>
                </div>
            </div>
        </div>

    </div>
    <!--wrap-->
    <script type="text/javascript">
        var _gaq = [["_setAccount", "UA-2946818-1"], ["_trackPageview"]];
        (function (d, t) {
            var g = d.createElement(t), s = d.getElementsByTagName(t)[0]; g.async = 1;
            g.src = ("https:" == location.protocol ? "//ssl" : "//www") + ".google-analytics.com/ga.js";
            s.parentNode.insertBefore(g, s)
        }(document, "script"));
    </script>
    <script>
        $(document).ready(function () {
            $("[data-select2-enable=true]").addClass("select2 select-clik");
        });

        $(document).ready(function () {
            $('.select2').select2({
                dropdownAutoWidth: true,
                width: '100%',
                //allowClear: true
            })
        });

        $(document).ready(function () {
            $(document).on("click", ".select2-search-clear-icon", function () {
                var sel2id = localStorage.getItem("sel2id");
                $('#' + sel2id).select2('close');
                $('#' + sel2id).select2('open');
            });

            $(document).on('click', '.select2', function () {
                debugger
                var key = $(this).parent().find('.select-clik').attr('id');
                localStorage.setItem("sel2id", key);
            });
        });
        </script>

</body>
</html>
