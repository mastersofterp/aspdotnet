/* ********************************************************************
 **********************************************************************
 * HTML Virtual Keyboard Interface Script - v1.39
 *   Copyright (c) 2010 - GreyWyvern
 *
 *  - Licenced for free distribution under the BSDL
 *          http://www.opensource.org/licenses/bsd-license.php
 *
 * Add a script-driven keyboard interface to text fields, password
 * fields and textareas.
 *
 * See http://www.greywyvern.com/code/javascript/keyboard for examples
 * and usage instructions.
 *
 * Version 1.39 - September 7, 2010
 *   - Assamese keyboard layout added
 *   - Kurdish keyboard layout added
 *   - Dari keyboard layout added
 *
 *   See full changelog at:
 *     http://www.greywyvern.com/code/javascript/keyboard.changelog.txt
 *
 * Keyboard Credits
 *   - Dari keyboard layout by Saif Fazel
 *   - Kurdish keyboard layout by Ara Qadir
 *   - Assamese keyboard layout by Kanchan Gogoi
 *   - Bulgarian BDS keyboard layout by Milen Georgiev
 *   - Basic Japanese Hiragana/Katakana keyboard layout by Damjan
 *   - Ukrainian keyboard layout by Dmitry Nikitin
 *   - Macedonian keyboard layout by Damjan Dimitrioski
 *   - Pashto keyboard layout by Ahmad Wali Achakzai (qamosona.com)
 *   - Armenian Eastern and Western keyboard layouts by Hayastan Project (www.hayastan.co.uk)
 *   - Pinyin keyboard layout from a collaboration with Lou Winklemann
 *   - Kazakh keyboard layout by Alex Madyankin
 *   - Danish keyboard layout by Verner Kjærsgaard
 *   - Slovak keyboard layout by Daniel Lara (www.learningslovak.com)
 *   - Belarusian, Serbian Cyrillic and Serbian Latin keyboard layouts by Evgeniy Titov
 *   - Bulgarian Phonetic keyboard layout by Samuil Gospodinov
 *   - Swedish keyboard layout by Håkan Sandberg
 *   - Romanian keyboard layout by Aurel
 *   - Farsi (Persian) keyboard layout by Kaveh Bakhtiyari (www.bakhtiyari.com)
 *   - Burmese keyboard layout by Cetanapa
 *   - Slovenian keyboard layout by Miran Zeljko
 *   - Hungarian keyboard layout by Antal Sall 'Hiromacu'
 *   - Arabic keyboard layout by Srinivas Reddy
 *   - Italian and Spanish (Spain) keyboard layouts by dictionarist.com
 *   - Lithuanian and Russian keyboard layouts by Ramunas
 *   - German keyboard layout by QuHno
 *   - French keyboard layout by Hidden Evil
 *   - Polish Programmers layout by moose
 *   - Turkish keyboard layouts by offcu
 *   - Dutch and US Int'l keyboard layouts by jerone
 *
 */
var VKI_attach, VKI_close;
  function VKI_buildKeyboardInputs() {
    var self = this;

    this.VKI_version = "1.39";
    this.VKI_showVersion = false;
    this.VKI_target = false;
    this.VKI_shift = this.VKI_shiftlock = false;
    this.VKI_altgr = this.VKI_altgrlock = false;
    this.VKI_dead = false;
    this.VKI_deadkeysOn = false;
    this.VKI_kts = this.VKI_kt = "Sym Math Test 1" //"Sym Math Test 2";  // Default keyboard layout
    this.VKI_langAdapt = true;  // Use lang attribute of input to select keyboard
    this.VKI_size = 1;  // Default keyboard size (1-5)
    this.VKI_sizeAdj = false;  // Allow user to adjust keyboard size
    this.VKI_clearPasswords = false;  // Clear password fields on focus
    this.VKI_imageURI ="../images/keyboard.png" //"keyboard.png";  // If empty string, use imageless mode
    this.VKI_clickless = 0;  // 0 = disabled, > 0 = delay in ms
    this.VKI_keyCenter = 1;

    this.VKI_isIE = /*@cc_on!@*/false;
    this.VKI_isIE6 = /*@if(@_jscript_version == 5.6)!@end@*/false;
    this.VKI_isIElt8 = /*@if(@_jscript_version < 5.8)!@end@*/false;
    this.VKI_isWebKit = RegExp("KHTML").test(navigator.userAgent);
    this.VKI_isOpera = RegExp("Opera").test(navigator.userAgent);
    this.VKI_isMoz = (!this.VKI_isWebKit && navigator.product == "Gecko");


    /* ***** i18n text strings ************************************* */
    this.VKI_i18n = {
      '00': "Virtual Keyboard Interface",
      '01': "Display virtual keyboard interface",
      '02': "Select keyboard layout",
      '03': "Dead keys",
      '04': "On",
      '05': "Off",
      '06': "Close the keyboard",
      '07': "Clear",
      '08': "Clear this input",
      '09': "Version",
      '10': "Adjust keyboard size"
    };


    /* ***** Create keyboards ************************************** */
    this.VKI_layout = {};

    // - Lay out each keyboard in rows of sub-arrays.  Each sub-array
    //   represents one key.
    //
    // - Each sub-array consists of four slots described as follows:
    //     example: ["a", "A", "\u00e1", "\u00c1"]
    //
    //          a) Normal character
    //          A) Character + Shift/Caps
    //     \u00e1) Character + Alt/AltGr/AltLk
    //     \u00c1) Character + Shift/Caps + Alt/AltGr/AltLk
    //
    //   You may include sub-arrays which are fewer than four slots.
    //   In these cases, the missing slots will be blanked when the
    //   corresponding modifier key (Shift or AltGr) is pressed.
    //
    // - If the second slot of a sub-array matches one of the following
    //   strings:
    //     "Tab", "Caps", "Shift", "Enter", "Bksp",
    //     "Alt" OR "AltGr", "AltLk"
    //   then the function of the key will be the following,
    //   respectively:
    //     - Insert a tab
    //     - Toggle Caps Lock (technically a Shift Lock)
    //     - Next entered character will be the shifted character
    //     - Insert a newline (textarea), or close the keyboard
    //     - Delete the previous character
    //     - Next entered character will be the alternate character
    //     - Toggle Alt/AltGr Lock
    //
    //   The first slot of this sub-array will be the text to display
    //   on the corresponding key.  This allows for easy localisation
    //   of key names.
    //
    // - Layout dead keys (diacritic + letter) should be added as
    //   arrays of two item arrays with hash keys equal to the
    //   diacritic.  See the "this.VKI_deadkey" object below the layout
    //   definitions.  In  each two item child array, the second item
    //   is what the diacritic would change the first item to.
    //
    // - To disable dead keys for a layout, simply assign true to the
    //   DDK property of the layout (DDK = disable dead keys).  See the
    //   Numpad layout below for an example.
    //
    // - Note that any characters beyond the normal ASCII set should be
    //   entered in escaped Unicode format.  (eg \u00a3 = Pound symbol)
    //   You can find Unicode values for characters here:
    //     http://unicode.org/charts/
    //
    // - To remove a keyboard, just delete it, or comment it out of the
    //   source code. If you decide to remove the US Int'l keyboard
    //   layout, make sure you change the default layout (this.VKI_kt)
    //   above so it references an existing layout.


    this.VKI_layout["US Int'l"] = [ // US International Keyboard
      [["`", "~"], ["1", "!", "\u00a1", "\u00b9"], ["2", "@", "\u00b2"], ["3", "#", "\u00b3"], ["4", "$", "\u00a4", "\u00a3"], ["5", "%", "\u20ac"], ["6", "^", "\u00bc"], ["7", "&", "\u00bd"], ["8", "*", "\u00be"], ["9", "(", "\u2018"], ["0", ")", "\u2019"], ["-", "_", "\u00a5"], ["=", "+", "\u00d7", "\u00f7"], ["Bksp", "Bksp"]],
      [["Tab", "Tab"], ["q", "Q", "\u00e4", "\u00c4"], ["w", "W", "\u00e5", "\u00c5"], ["e", "E", "\u00e9", "\u00c9"], ["r", "R", "\u00ae"], ["t", "T", "\u00fe", "\u00de"], ["y", "Y", "\u00fc", "\u00dc"], ["u", "U", "\u00fa", "\u00da"], ["i", "I", "\u00ed", "\u00cd"], ["o", "O", "\u00f3", "\u00d3"], ["p", "P", "\u00f6", "\u00d6"], ["[", "{", "\u00ab"], ["]", "}", "\u00bb"], ["\\", "|", "\u00ac", "\u00a6"]],
      [["Caps", "Caps"], ["a", "A", "\u00e1", "\u00c1"], ["s", "S", "\u00df", "\u00a7"], ["d", "D", "\u00f0", "\u00d0"], ["f", "F"], ["g", "G"], ["h", "H"], ["j", "J"], ["k", "K"], ["l", "L", "\u00f8", "\u00d8"], [";", ":", "\u00b6", "\u00b0"], ["'", '"', "\u00b4", "\u00a8"], ["Enter", "Enter"]],
      [["Shift", "Shift"], ["z", "Z", "\u00e6", "\u00c6"], ["x", "X"], ["c", "C", "\u00a9", "\u00a2"], ["v", "V"], ["b", "B"], ["n", "N", "\u00f1", "\u00d1"], ["m", "M", "\u00b5"], [",", "<", "\u00e7", "\u00c7"], [".", ">"], ["/", "?", "\u00bf"], ["Shift", "Shift"]],
      [[" ", " ", " ", " "], ["Alt", "Alt"]]
    ]; this.VKI_layout["US Int'l"].lang = ["en"];
	
	/* QuHno Mod START */

    this.VKI_layout["Sym Math 2200-FF"] = [ // Mathematics Keyboard
      [["\u2200"], ["\u2201"], ["\u2202"], ["\u2203"], ["\u2204"], ["\u2205"], ["\u2206"], ["\u2207"], ["\u2208"], ["\u2209"], ["\u220A"], ["\u220B"], ["\u220C"], ["\u220D"] ,["\u220E"], ["\u220F"], ["\u2210"], ["\u2211"], ["Bksp","Bksp"]],
	  [["Tab","Tab"], ["\u2212"], ["\u2213"], ["\u2214"], ["\u2215"], ["\u2216"], ["\u2217"], ["\u2218"], ["\u2219"], ["\u221A"], ["\u221B"], ["\u221C"], ["\u221D"], ["\u221E"], ["\u221F"], ["\u2220"], ["\u2221"], ["\u2222"], ["\u2223"]],
	  [["\u2224"], ["\u2225"], ["\u2226"], ["\u2227"], ["\u2228"], ["\u2229"],["\u222A"], ["\u222B"], ["\u222C"], ["\u222D"], ["\u222E"], ["\u222F"], ["\u2230"], ["\u2231"], ["\u2232"], ["\u2233"], ["Enter","Enter"]],
	  [["\u223A"], ["\u223B"], ["\u223C"], ["\u223D"], ["\u223E"], ["\u223F"],  ["\u2240"], ["\u2241"], ["\u2242"], ["\u2243"], ["\u2244"], ["\u2245"], ["\u2246"], ["\u2247"], ["\u2248"], ["\u2249"], ["\u2234"], ["\u2235"], ["\u2236"], ["\u2237"]],
	  [["\u2238"], ["\u2239"], ["\u224A"], ["\u224B"], ["\u224C"], ["\u224D"], ["\u224E"], ["\u224F"], ["\u2250"], ["\u2251"], ["\u2252"], ["\u2253"], ["\u2254"], ["\u2255"], ["\u2256"], ["\u2257"], ["\u2258"], ["\u2259"], ["\u225A"], ["\u225B"]],
	  [["\u225C"], ["\u225D"], ["\u225E"], ["\u225F"], ["\u2260"], ["\u2261"], ["\u2262"], ["\u2263"], ["\u2264"], ["\u2265"], ["\u2266"], ["\u2267"], ["\u2268"], ["\u2269"], ["\u226A"], ["\u226B"], ["\u226C"], ["\u226D"], ["\u226E"], ["\u226F"]],
	  [["\u2270"], ["\u2271"], ["\u2272"], ["\u2273"], ["\u2274"], ["\u2275"], ["\u2276"], ["\u2277"], ["\u2278"], ["\u2279"], ["\u227A"], ["\u227B"], ["\u227C"], ["\u227D"],  ["\u227E"], ["\u227F"], ["\u2280"], ["\u2281"], ["\u2282"], ["\u2283"]],
	  [["\u2284"], ["\u2285"], ["\u2286"], ["\u2287"], ["\u2288"], ["\u2289"], ["\u228A"], ["\u228B"], ["\u228C"], ["\u228D"], ["\u228E"], ["\u228F"], ["\u2290"], ["\u2291"], ["\u2292"], ["\u2293"], ["\u2294"], ["\u2295"], ["\u2296"], ["\u2297"]],
	  [["\u2298"], ["\u2299"], ["\u229A"], ["\u229B"], ["\u229C"], ["\u229D"], ["\u229E"], ["\u229F"], ["\u22A0"], ["\u22A1"], ["\u22A2"], ["\u22A3"], ["\u22A4"], ["\u22A5"], ["\u22A6"], ["\u22A7"], ["\u22A8"], ["\u22A9"], ["\u22AA"]],
	  [["\u22AB"], ["\u22AC"], ["\u22AD"], ["\u22AE"], ["\u22AF"], ["\u22B0"], ["\u22B1"], ["\u22B2"], ["\u22B3"], ["\u22B4"], ["\u22B5"], ["\u22B6"], ["\u22B7"], ["\u22B8"], ["\u22B9"], ["\u22BA"], ["\u22BB"], ["\u22BC"], ["\u22BD"], ["\u22BE"]],
	  [["\u22BF"], ["\u22C0"], ["\u22C1"], ["\u22C2"], ["\u22C3"], ["\u22C4"], ["\u22C5"], ["\u22C6"], ["\u22C7"], ["\u22C8"], ["\u22C9"], ["\u22CA"], ["\u22CB"], ["\u22CC"], ["\u22CD"], ["\u22CE"], ["\u22CF"], ["\u22D0"], ["\u22D1"], ["\u22D2"]],
	  [["\u22D3"], ["\u22D4"], ["\u22D5"], ["\u22D6"], ["\u22D7"], ["\u22D8"], ["\u22D9"], ["\u22DA"], ["\u22DB"], ["\u22DC"], ["\u22DD"], ["\u22DE"], ["\u22DF"], ["\u22E0"], ["\u22E1"], ["\u22E2"], ["\u22E3"], ["\u22E4"], ["\u22E5"], ["\u22E6"]],
	  [["\u22E7"], ["\u22E8"], ["\u22E9"], ["\u22EA"], ["\u22EB"], ["\u22EC"], ["\u22ED"], ["\u22EE"], ["\u22EF"], ["\u22F0"], ["\u22F1"], ["\u22F2"], ["\u22F3"], ["\u22F4"], ["\u22F5"], ["\u22F6"], ["\u22F7"], ["\u22F8"], ["\u22F9"]],
	  [["\u22FA"], ["\u22FB"], ["\u22FC"], ["\u22FD"], ["\u22FE"], ["\u22FF"], [[" "], [" "], [" "], [" "]]]
    ];

	this.VKI_layout['Sym Math Frac sub sup'] = [
	  [/* 1/4 */["\u00BC","\u00BC","\u00BC"],/* 1/2 */["\u00BD","\u00BD","\u00BD"],/* 3/4 */["\u00BE","\u00BE","\u00BE"],/* 1/3 */["\u2153","\u2153","\u2153"],/* ZERO*/["0","\u2080","\u2080"], ["(",/*SUPERSCRIPT LEFT PARENTHESIS*/"\u207D", /*SUBSCRIPT LEFT PARENTHESIS*/"\u208D"], [")",/*SUPERSCRIPT RIGHT PARENTHESIS*/"\u207E", /*SUBSCRIPT RIGHT PARENTHESIS*/"\u208E"],/* MINUS*/["(","\u207B","\u208B"], ["n", /*SUPERSCRIPT LATIN SMALL LETTER N*/"\u207F"], ["r", " \u036C", /*LATIN SUBSCRIPT SMALL LETTER R*/"\u1D63"], ["\u03B2", /*GREEK SUBSCRIPT SMALL LETTER BETA*/"\u1D66"], ["Bksp", "Bksp"]],
	  
	  [/* 2/3 */["\u2154","\u2154"],/* 1/5 */["\u2155","\u2155"],/* 2/5 */["\u2156","\u2156"],/* 3/5 */["\u2157","\u2157"],/* SEVEN*/["7","\u2077","\u2087"],/* EIGHT*/["8","\u2078","\u2088"],/* NINE*/["9","\u2079","\u2089"],/* PLUS*/["+","\u207A","\u208A"], ["a", " \u0363", /*LATIN SUBSCRIPT SMALL LETTER A*/"\u2090"], ["u", " \u0367", /*LATIN SUBSCRIPT SMALL LETTER U*/"\u1D64"], ["\u03B3", /*GREEK SUBSCRIPT SMALL LETTER GAMMA*/"\u1D67"],["\u03C1", /*GREEK SUBSCRIPT SMALL LETTER RHO*/"\u1D68"],  ["Caps", "Caps"]],
	  
	  [/* 4/5 */["\u2158","\u2158"],/* 1/6 */["\u2159","\u2159"],/* 5/6 */["\u215A","\u215A"],/* 1/8 */["\u215B","\u215B"],/* FOUR*/["4","\u2074","\u2084"],/* FIVE*/["5","\u2075","\u2085"],/* SIX*/["6","\u2076","\u2086"],["i",/*SUPERSCRIPT LATIN SMALL LETTER I*/"\u2071", /*LATIN SUBSCRIPT SMALL LETTER I*/"\u1D62"], ["e", " \u0364", /*LATIN SUBSCRIPT SMALL LETTER E*/"\u2091"], ["v", " \u036E", /*LATIN SUBSCRIPT SMALL LETTER V*/"\u1D65"],["\u03C6", /*GREEK SUBSCRIPT SMALL LETTER PHI*/"\u1D69"],  ["Shift", "Shift"]],
	  
	  [/* 3/8 */["\u215C","\u215C"],/* 5/8 */["\u215D","\u215D"],/* 7/8 */["\u215E","\u215E"],/*FRACTION SLASH*/["\u2044","\u2044"],/* ONE*/["\u00B9","\u2081"],/* TWO */["\u00B2","\u2082"],/* THREE */["\u00B3","\u2083"],[/*SUPERSCRIPT EQUALS SIGN*/"\u207C", /*SUBSCRIPT EQUALS SIGN*/"\u208C"],["o", " \u0366", /*LATIN SUBSCRIPT SMALL LETTER O*/"\u2092"], ["x", " \u036F", /*LATIN SUBSCRIPT SMALL LETTER X*/"\u2093"], ["\u03C7", /*GREEK SUBSCRIPT SMALL LETTER CHI*/"\u1D6A"], ["Enter", "Enter"]],
	  [[" ", " "],["Alt","Alt"]]
    ];
	

	this.VKI_layout["Sym Math Test 1"] = [ 
	  [["^","°"],  ["1",  "\u00B9","\u2081", "\u2091"], ["2",  "\u00B2","\u2082", "\u2092"], ["3",  "\u00B3","\u2083", "\u1D63"], ["4",  "\u2074","\u2084", "\u1D64"], ["5", "\u2075","\u2085", "\u2093"], ["6", "\u2076", "\u2086", "\u1D66"], ["7", "\u2077", "\u2087", "\u1D67"], ["8", "\u2078", "\u2088", "\u1D68"], ["9", "\u2079", "\u2089", "\u1D69"], ["0", "\u2070", "\u2080", "\u2090"], ["+", "\u207A", "\u208A", "\u1D6A"], ["\u2212",  "\u207B","\u208B"], ["=", "\u207C", "\u208C"], ["(", "\u207D", "\u208D"], [")", "\u207E", "\u208E"], ["i", "\u2071", "\u1D62"], ["n", "\u207F"], ["Bksp", "Bksp"]],
	  [["Tab", "Tab"],/*=*/["=", "\u2260", "\u2261", "\u2262"],["\u2263"],/*<*/["\u003C", "\u226E", "\u226A", "\u22D8"], /*>*/["\u003E", "\u226F", "\u226B", "\u22D9"], /*<=*/["\u2264", "\u2270", "\u2266", "\u2268", "\u2270"], /*>=*/ ["\u2265", "\u2271", "\u2267", "\u2269", "\u2271"], ["\u00B1"], ["\u00F7"], ["\u00D7"],  ["\u2044", "\u2215"], ["\u221A", "\u221B", "\u221C"], ["\u221E", "\u2135"], ["\u03C0", "\u03A0", "\u03D6"]],
	  [["Caps", "Caps"], /*integral*/["\u222B", "\u222C", "\u222D", "\u2A0C"], /*contour integral*/["\u222E", "\u222F", "\u2230"], /*clockwise Integral*/["\u2231", "\u2A11", "\u2232", "\u2233"], ["\u2032", "\u2033", "\u2034"], ["\u2234"], ["\u22C5"],  ["\u2297"], ["\u2295"], ["Enter", "Enter"]],
	  [["Shift", "Shift"], ["\u2329"], ["\u232A"], ["\u2308"], ["\u2309"], ["\u230A"], ["\u230B"], ["x\u0304","X\u0304"], ["d\u0304","D\u0304"], ["Shift", "Shift"]],
	  [[" ", " ", " ", " "], ["Something", "Alt"],["AltLk", "AltLk"]]
  ];
  
this.VKI_layout["Sym Math Test 2"] = [ 

[
["^","°"], ["+", "\u207A", "\u208A", "\u1D6A"],/*-*/ ["\u2212",  "\u207B","\u208B"], ["=", "\u207C", "\u208C"], ["(", "\u207D", "\u208D"], [")", "\u207E", "\u208E"], ["1",  "\u00B9","\u2081", "\u2091"], ["2",  "\u00B2","\u2082", "\u2092"], ["3",  "\u00B3","\u2083", "\u1D63"], ["4",  "\u2074","\u2084", "\u1D64"], ["5", "\u2075","\u2085", "\u2093"], ["6", "\u2076", "\u2086", "\u1D66"], ["7", "\u2077", "\u2087", "\u1D67"], ["8", "\u2078", "\u2088", "\u1D68"], ["9", "\u2079", "\u2089", "\u1D69"], ["0", "\u2070", "\u2080", "\u2090"], ["i", "\u2071", "\u1D62"], ["n","\u207F"], ["Bksp", "Bksp"]
],
[
[/*PLUS OR MINUS */"\u00B1",/*MINUS-OR-PLUS SIGN*/"\u2213"],
["+",/*CIRCLED PLUS*/"\u2295",/*SQUARED PLUS*/"\u229E"],
[/*MINUS */"\u2212",/*CIRCLED MINUS*/"\u2296",/*SQUARED MINUS*/"\u229F"],
[/*TIMES X*/"\u00D7",/*CIRCLED TIMES*/"\u2297",/*SQUARED TIMES*/"\u22A0"],
[/*DIVISION SLASH*/"\u2215",/*CIRCLED DIVISION SLASH*/"\u2298",/*÷*/"\u00F7",/*FRACTION SLASH*/"\u2044"],
[/*ASTERISK OPERATOR*/"\u2217",/*CIRCLED ASTERISK OPERATOR*/"\u229B",/*SQUARED ASTERISK*/"\u29C6"],
[/*RING OPERATOR*/"\u2218",/*CIRCLED RING OPERATOR*/"\u229A",/*SQUARED SMALL CIRCLE*/"\u29C7"],
[/*BULLET OPERATOR */"\u2219"],
[/*DOT OPERATOR*/"\u22C5",/*CIRCLED DOT OPERATOR*/"\u2299",/*SQUARED DOT OPERATOR*/"\u22AA"],
[/*SQUARE ROOT RADICAL */"\u221A",/*CUBE ROOT*/"\u221B",/*FOURTH ROOT*/"\u221C"],
[/*INFINITY*/"\u221E",/*ALEF INFINITY SYMBOL */"\u2135"],
[/*FUNCTION ITALIC F */"\u0192",/*PRIME (single quote) */"\u2032",/*DOUBLE PRIME (double quote) */"\u2033",/*TRIPLE PRIME (triple quote) */"\u2034"],
[/*THEREFORE*/"\u2234",/*BECAUSE*/"\u2235",/*PROPORTIONAL TO*/"\u221D",/*END OF PROOF*/"\u220E"],
[],[],[],[],[],["Tab", "Tab"]
],
[
["\u2261","\u2263"],/*<, ≤,precedes, precedes=*/["<","\u2264","\u227A","\u227C",],[">","\u2265","\u227B","\u227D"],[],[],[],[],[],[],[],[],[],[],[],[],[],[],[],["Caps", "Caps"]
],
[
[],[],[],[],[],[],[],[],[],[],[],[],[],[],[], ["Enter", "Enter"]
],
[
["Shift", "Shift"], [],[],[],[],[],[],[],[],[],[],[],[],[",",";"],[".",":"],["\u00AC","\u00AC","\u00AC","\u00AC"],  ["Shift", "Shift"]
],
[[" ", " ", " ", " "], ["Alt", "Alt"],["AltLk", "AltLk"]]
  ];this.VKI_size = 5;
  
  
this.VKI_layout["Sym Math Test 3"] = [ /**Normal, Subscript and Superscript**/

[
["^", "°"],
["0", /*SUBSCRIPT ZERO*/"\u2080", /*SUPERSCRIPT ZERO*/"\u2070"],  
["1", /*SUBSCRIPT ONE*/"\u2081", /*SUPERSCRIPT ONE*/"\u00B9"], 
["2", /*SUBSCRIPT TWO*/"\u2082", /*SUPERSCRIPT TWO*/"\u00B2"], 
["3", /*SUBSCRIPT THREE*/"\u2083", /*SUPERSCRIPT THREE*/"\u00B3"],
["4", /*SUBSCRIPT FOUR*/"\u2084", /*SUPERSCRIPT FOUR*/"\u2074"], 
["5", /*SUBSCRIPT FIVE*/"\u2085", /*SUPERSCRIPT FIVE*/"\u2075"],
["6", /*SUBSCRIPT SIX*/"\u2086", /*SUPERSCRIPT SIX*/"\u2076"],
["7", /*SUBSCRIPT SEVEN*/"\u2087", /*SUPERSCRIPT SEVEN*/"\u2077"],
["8", /*SUBSCRIPT EIGHT*/"\u2088", /*SUPERSCRIPT EIGHT*/"\u2078"], 
["9", /*SUBSCRIPT NINE*/"\u2089", /*SUPERSCRIPT NINE*/"\u2079"],
["+", /*SUBSCRIPT PLUS SIGN*/"\u208A", /*SUPERSCRIPT PLUS SIGN*/"\u207A"], 
["-", /*SUBSCRIPT MINUS*/"\u208B", /*SUPERSCRIPT MINUS*/"\u207B"],
["=", /*SUBSCRIPT EQUALS SIGN*/"\u208C", /*SUPERSCRIPT EQUALS SIGN*/"\u207C"],
["(", /*SUBSCRIPT LEFT PARENTHESIS*/"\u208D", /*SUPERSCRIPT LEFT PARENTHESIS*/"\u207D"], 
[")", /*SUBSCRIPT RIGHT PARENTHESIS*/"\u208E", /*SUPERSCRIPT RIGHT PARENTHESIS*/"\u207E"],
["n", "", /*SUPERSCRIPT LATIN SMALL LETTER N*/"\u207F"], 
["a", /*LATIN SUBSCRIPT SMALL LETTER A*/"\u2090"],
["e", /*LATIN SUBSCRIPT SMALL LETTER E*/"\u2091"], 
["o", /*LATIN SUBSCRIPT SMALL LETTER O*/"\u2092"], 
["i", /*LATIN SUBSCRIPT SMALL LETTER I*/"\u1D62", /*SUPERSCRIPT LATIN SMALL LETTER I*/"\u2071"],
["r", /*LATIN SUBSCRIPT SMALL LETTER R*/"\u1D63"], 
["u", /*LATIN SUBSCRIPT SMALL LETTER U*/"\u1D64"], 
["v", /*LATIN SUBSCRIPT SMALL LETTER V*/"\u1D65"], 
["x", /*LATIN SUBSCRIPT SMALL LETTER X*/"\u2093"], 
["\u03B2", /*GREEK SUBSCRIPT SMALL LETTER BETA*/"\u1D66"], 
["\u03B3", /*GREEK SUBSCRIPT SMALL LETTER GAMMA*/"\u1D67"], 
["\u03C1", /*GREEK SUBSCRIPT SMALL LETTER RHO*/"\u1D68"], 
["\u03C6", /*GREEK SUBSCRIPT SMALL LETTER PHI*/"\u1D69"], 
["\u03C7", /*GREEK SUBSCRIPT SMALL LETTER CHI*/"\u1D6A"],
["Bksp", "Bksp"]
],

/**Common Arithmetic &amp; Algebra **/

[
["Tab", "Tab"],
/*MICRO MU SYMBOL*/["\u00B5"], 
/*PLUS OR MINUS*/["\u00B1"], 
/*DIVISION SIGN*/["\u00F7"], 
/*TIMES X*/["\u00D7"], 
/*MINUS*/["\u2212"], 
/*DIVISION SLASH*/["\u2215"], 
/*FRACTION SLASH*/["\u2044"], 
/*SQUARE ROOT RADICAL*/["\u221A"], 
/*CUBE ROOT*/["\u221B"], 
/*FOURTH ROOT*/["\u221C"], 
/*INFINITY*/["\u221E", /*ALEF INFINITY SYMBOL*/"\u2135"], 
/*FUNCTION ITALIC F*/["\u0192"], 
/*PRIME (single quote,  degree minutes, feet)*/["\u2032", /*DOUBLE PRIME (double quote, degree seconds, inches)*/"\u2033", 
/*TRIPLE PRIME (triple quote)*/"\u2034"], 
/*DOT OPERATOR*/["\u22C5"], 
["(", "{", /*LEFT ANGLE BRACKET*/"\u2329"],
[")", "}", /*RIGHT ANGLE BRACKET*/"\u232A"],
["[", /*LEFT FLOOR BRACKET*/"\u230A", /*LEFT CEILING BRACKET*/"\u2308"], 
["]", /*RIGHT FLOOR BRACKET*/"\u230B",  /*RIGHT CEILING BRACKET*/"\u2309"], 
/*CIRCLED PLUS (Direct Sum)*/["\u2295"], 
/*CIRCLED TIMES (Vector Product)*/["\u2297"],
/*PER MILLE (1/1000th)*/["\u2030"],
["Enter", "Enter"]
],



[
["Caps", "Caps"],
/**Common Statistics**/
/*LOWER CASE MU (Mean)*/["\u03BC"], 
/*LOWER CASE SIGMA (Standard Deviation)*/["\u03C3"], 
/*LOWER CASE CHI*/["\u03C7"], 
/*CAPITAL PI N-ARY PRODUCT*/["\u2211"], 
/*N-ARY COPRODUCT (upside down capital pi)*/["\u2210"],
/**X-Bar, P-Hat and D-Bar**/
/*X-Bar (Average)*/["x\u0304"], 
/*P-Hat*/["p\u0302"], 
/*D-Bar*/["D\u0304"],
/**Letter Symbols **/
/*GREEK SMALL LETTER PI*/["\u03C0"],
/*GREEK CAPITAL LETTER PI*/["\u03A0"],
/*GREEK PI SYMBOL (Omega PI)*/["\u03D6"],
/*WEIERSTRASS POWER SET (Script Capital P)*/["\u2118"], 
/*IMAGINARY NUMBER (Blackletter I)*/["\u2111"], 
/*REAL NUMBER (Blackletter R)*/["\u211C"], 
/*DOUBLE-STRUCK REAL NUMBER (Double R)*/["\u211D"], 
/*COMPLEX NUMBERS (Double C)*/["\u2102"], 
/*NATURAL NUMBERS (Double N)*/["\u2115"], 
/*PRIME NUMBERS (Double P)*/["\u2119"], 
/*RATIONAL NUMBERS (Double Q)*/["\u211A"], 
/*INTEGERS (Double Z)*/["\u2124"]
],

/**Calculus**/


/**Common Calculus**/

[
["Shift", "Shift"], 
/*INTEGRAL*/["\u222B", /*DOUBLE INTEGRAL*/"\u222C", /*TRIPLE INTEGRAL*/"\u222D", /*QUADRUPLE INTEGRAL*/"\u2A0C"], 
/*CONTOUR INTEGRAL*/["\u222E", /*SURFACE INTEGRAL*/"\u222F", /*VOLUME INTEGRAL*/"\u2230"], 
/*CLOCKWISE INTEGRAL*/["\u2231", /*ANTICLOCKWISE INTEGRAL*/"\u2A11", /*CLOCKWISE CONTOUR INTEGRAL*/"\u2232", /*ANTICLOCKWISE CONTOUR INTEGRAL*/"\u2233"],
/*PARTIAL DIFFERENTIAL*/["\u2202"], 
/*INCREMENT (Difference or capital Delta)*/["\u2206", /*NABLA (Backward Difference, Grad or upside down triangle)*/"\u2207"],
["Shift", "Shift"]
],

[["Alt", "Alt"], [" ", " ", " ", " "], ["AltLk", "AltLk"]],



/**Logic &amp; Set Theory**/

[
/*FOR ALL (Upside-down A)*/["\u2200"], 
/*COMPLEMENT (Thin C)*/["\u2201"], 
/*THERE EXISTS (Backwards E)*/["\u2203"], 
/*THERE DOES NOT EXIST (Backwards E with slash)*/["\u2204"], 
/*EMPTY SET (O slash)*/["\u2205"], 
/*(+DEAD) NOT SYMBOL (Corner)*/["\u00AC"], 
/*(+DEAD) TILDE (Alternate Not Symbol)*/["\u007E"], 
/*LOGICAL AND (Wedge or Upside down V Symbol)*/["\u2227"], 
/*LOGICAL OR (V Symbol)*/["\u2228"], 
/*XOR*/["\u22BB"], 
/*NAND*/["\u22BC"], 
/*NOR*/["\u22BD"], 
/*INTERSECTION (Cap or Upside Down U)*/["\u2229"], 
/*UNION (Cup or U Symbol)*/["\u222A"], 
/*ELEMENT OF*/["\u2208"], 
/*NOT AN ELEMENT OF*/["\u2209"], 
/*SMALL ELEMENT OF*/["\u220A"], 
/*CONTAINS AS MEMBER*/["\u220B"], 
/*DOES NOT CONTAIN AS MEMBER*/["\u220C"], 
/*SMALL CONTAINS AS MEMBER*/["\u220D"], 
/*SET MINUS*/["\u2216"], 
/*SUBSET OF (Sideways U with cap to left)*/["\u2282"], /*NOT A SUBSET OF (Subset with Slash)*/["\u2284"], 
/*SUPERSET OF (Sideways U with cap to right)*/["\u2283"], /*NOT A SUPERSET OF (Superset with slash)*/["\u2285"], 
/*SUBSET OF OR EQUAL TO (Subset with line below)*/["\u2286"], /*NEITHER A SUBSET OF NOR EQUAL TO*/["\u2288"], 
/*SUPERSET OF OR EQUAL TO (Superset with line below)*/["\u2287"], /*NEITHER A SUPERSET OF NOR EQUAL TO*/["\u2289"], 
/*SUBSET OF WITH NOT EQUAL TO*/["\u228A"], 
/*SUPERSET OF WITH NOT EQUAL TO*/["\u228B"], 
/*DIAMOND OPERATOR (Possibility)*/["\u22C4"], 
/*ASYMPTOTICALLY EQUAL TO (One to one Correspondence)*/["\u2243"], 
/*NOT ASYMPTOTICALLY EQUAL TO*/["\u2244"], 
/*MULTISET (U with arrow)*/["\u228C"], 
/*MULTISET MULTIPLICATION (U with dot in center)*/["\u228D"], 
/*MULTISET UNION (U with plus in center)*/["\u228E"], 
/*DOUBLE SUBSET*/["\u22D0"], 
/*DOUBLE SUPERSET*/["\u22D1"], 
/*DOUBLE INTERSECTION*/["\u22D2"], 
/*DOUBLE UNION*/["\u22D3"], 
/*N-ARY LOGICAL AND*/["\u22C0"], 
/*N-ARY LOGICAL OR*/["\u22C1"], 
/*N-ARY INTERSECTION*/["\u22C2"], 
/*N-ARY UNION*/["\u22C3"], 
/*CURLY LOGICAL OR*/["\u22CE"], 
/*CURLY LOGICAL AND*/["\u22CF"], 
/*CIRCLED PLUS (Direct Sum)*/["\u2295"], 
/*CIRCLED TIMES (Vector Product)*/["\u2297"], 
/*CIRCLED MINUS*/["\u2296"], 
/*CIRCLED DIVISION SLASH*/["\u2298"],
/*CIRCLED DOT OPERATOR*/["\u2299"], 
/*CIRCLED RING OPERATOR*/["\u229A"], 
/*CIRCLED ASTERISK OPERATOR*/["\u229B"], 
/*CIRCLED EQUALS*/["\u229C"], 
/*CIRCLED DASH*/["\u229D"]
],

/**Geometric Symbols **/
/**Angles and Lines**/

[
/*RIGHT ANGLE*/["\u221F"], 
/*ANGLE*/["\u2220"], 
/*MEASURED ANGLE*/["\u2221"], 
/*SPHERICAL ANGLE*/["\u2222"], 
/*DIVIDES*/["\u2223"], 
/*DOES NOT DIVIDE*/["\u2224"], 
/*PARALLEL TO*/["\u2225"], 
/*NOT PARALLEL TO*/["\u2226"], 
/*RIGHT ANGLE WITH ARC*/["\u22BE"], 
/*RIGHT TRIANGLE*/["\u22BF"], 
/*UP TACK (Perpendicular)*/["\u22A5"], 
/*RIGHT TACK*/["\u22A2"], 
/*LEFT TACK*/["\u22A3"], 
/*DOWN TACK*/["\u22A4"]
],

/**Logical Proofs**/
[
/*THEREFORE (Triangular Dots)*/["\u2234"], 
/*BECAUSE (Upside down Triangular Dots)*/["\u2235"], 
/*PROPORTIONAL TO*/["\u221D"], 
/*END OF PROOF (solid rectangle)*/["\u220E"]
],

/**Common Equivalence and Proportion Operators**/


/**Common Equivalence Operators**/
[
/*EQUALS*/["="],/*NOT EQUALS*/["\u2260"],
/*LESS THAN*/["\u003C"], /*NOT LESS-THAN (slash)*/["\u226E"], 
/*IDENTICAL TO (three lines)*/["\u2261"], /*NOT IDENTICAL TO*/["\u2262"],
/*STRICTLY EQUIVALENT TO*/["\u2263"], 
/*GREATER THAN*/["\u003E"], /*NOT GREATER-THAN (slash)*/["\u226F"], 
/*LESS THAN OR EQUAL TO*/["\u2264"], /*NEITHER LESS-THAN NOR EQUAL TO*/["\u2270"], 
/*GREATER THAN OR EQUAL TO*/["\u2265"], /*NEITHER GREATER-THAN NOR EQUAL TO*/["\u2271"], 
/*APPROXIMATELY EQUAL*/["\u2245"], 
/*ALMOST EQUAL (ASYMPTOTIC)*/["\u2248"], /*NOT ALMOST EQUAL TO*/["\u2249"], 
/*TILDE SIMILAR TO*/["\u223C"], /*NOT TILDE*/["\u2241"],  
],

/**Other Equivalence Symbols**/

[
/*LESS-THAN OVER EQUAL TO*/["\u2266"], 
/*GREATER-THAN OVER EQUAL TO*/["\u2267"], 
/*LESS-THAN BUT NOT EQUAL TO*/["\u2268"], 
/*GREATER-THAN BUT NOT EQUAL TO*/["\u2269"], 
/*MUCH LESS-THAN*/["\u226A"], 
/*MUCH GREATER-THAN*/["\u226B"], 
/*BETWEEN*/["\u226C"], 
/*NOT EQUIVALENT TO*/["\u226D"], 
/*LESS-THAN OR EQUIVALENT TO*/["\u2272"], /*NEITHER LESS-THAN NOR EQUIVALENT TO*/["\u2274"], 
/*GREATER-THAN OR EQUIVALENT TO*/["\u2273"], /*NEITHER GREATER-THAN NOR EQUIVALENT TO*/["\u2275"], 
/*LESS-THAN OR GREATER-THAN*/["\u2276"], /*NEITHER LESS-THAN NOR GREATER-THAN*/["\u2278"], 
/*GREATER-THAN OR LESS-THAN*/["\u2277"], /*NEITHER GREATER-THAN NOR LESS-THAN*/["\u2279"], 

/*MINUS TILDE*/["\u2242"], 
/*ASYMPTOTICALLY EQUAL TO*/["\u2243"], /*NOT ASYMPTOTICALLY EQUAL TO*/["\u2244"], 
/*APPROXIMATELY BUT NOT ACTUALLY EQUAL TO*/["\u2246"], /*NEITHER APPROXIMATELY NOR ACTUALLY EQUAL TO*/["\u2247"],  
/*ALMOST EQUAL OR EQUAL TO*/["\u224A"], 
/*TRIPLE TILDE*/["\u224B"], 
/*ALL EQUAL TO*/["\u224C"]
],

/****Other Mathematical Symbols****/

[

/*DOT PLUS*/["\u2214"], 
/*ASTERISK OPERATOR*/["\u2217"], 
/*RING OPERATOR*/["\u2218"], 
/*BULLET OPERATOR*/["\u2219"], 
/*PROPORTIONAL TO*/["\u221D"], 
/*RATIO*/["\u2236"], 
/*PROPORTION*/["\u2237"], 
/*DOT MINUS*/["\u2238 "], 
/*EXCESS*/["\u2239"], 
/*GEOMETRIC PROPORTION*/["\u223A"], 
/*HOMOTHETIC*/["\u223B"], 
/*TILDE OPERATOR*/["\u223C"], 
/*REVERSED TILDE*/["\u223D"], 
/*INVERTED LAZY S*/["\u223E"], 
/*SINE WAVE*/["\u223F"], 
/*WREATH PRODUCT*/["\u2240"], 
/*EQUIVALENT TO*/["\u224D"], 
/*GEOMETRICALLY EQUIVALENT TO*/["\u224E"], 
/*DIFFERENCE BETWEEN*/["\u224F"], 
/*APPROACHES THE LIMIT*/["\u2250"], 
/*GEOMETRICALLY EQUAL TO*/["\u2251"], 
/*APPROXIMATELY EQUAL TO OR THE IMAGE OF*/["\u2252"], 
/*IMAGE OF OR APPROXIMATELY EQUAL TO*/["\u2253"], 
/*COLON EQUALS*/["\u2254"], 
/*EQUALS COLON*/["\u2255"], 
/*RING IN EQUAL TO*/["\u2256"], 
/*RING EQUAL TO*/["\u2257"], 
/*CORRESPONDS TO*/["\u2258"], 
/*ESTIMATES*/["\u2259"], 
/*EQUIANGULAR TO*/["\u225A"], 
/*STAR EQUALS*/["\u225B"], 
/*DELTA EQUAL TO*/["\u225C"], 
/*EQUAL TO BY DEFINITION*/["\u225D"], 
/*MEASURED BY*/["\u225E"], 
/*QUESTIONED EQUAL TO*/["\u225F"], 
/*PRECEDES*/["\u227A"], 
/*SUCCEEDS*/["\u227B"], 
/*PRECEDES OR EQUAL TO*/["\u227C"], 
/*SUCCEEDS OR EQUAL TO*/["\u227D"], 
/*PRECEDES OR EQUIVALENT TO*/["\u227E"], 
/*SUCCEEDS OR EQUIVALENT TO*/["\u227F"], 
/*DOES NOT PRECEDE*/["\u2280"], 
/*DOES NOT SUCCEED*/["\u2281"], 
/*SQUARE IMAGE OF*/["\u228F"], 
/*SQUARE ORIGINAL OF*/["\u2290"], 
/*SQUARE IMAGE OF OR EQUAL TO*/["\u2291"], 
/*SQUARE ORIGINAL OF OR EQUAL TO*/["\u2292"], 
/*SQUARE CAP*/["\u2293"], 
/*SQUARE CUP*/["\u2294"], 
/*PRECEDES UNDER RELATION*/["\u22B0"], 
/*SUCCEEDS UNDER RELATION*/["\u22B1"], 
/*NORMAL SUBGROUP OF*/["\u22B2"], 
/*CONTAINS AS NORMAL SUBGROUP*/["\u22B3"], 
/*NORMAL SUBGROUP OF OR EQUAL TO*/["\u22B4"], 
/*CONTAINS AS NORMAL SUBGROUP OR EQUAL TO*/["\u22B5"], 
/*ORIGINAL OF*/["\u22B6"], 
/*IMAGE OF*/["\u22B7"], 
/*MULTIMAP*/["\u22B8"], 
/*HERMITIAN CONJUGATE MATRIX*/["\u22B9"], 
/*INTERCALATE*/["\u22BA"], 
/*DIAMOND OPERATOR*/["\u22C4"], 
/* DOT OPERATOR*/["\u22C5"], 
/*STAR OPERATOR*/["\u22C6"], 
/*DIVISION TIMES*/["\u22C7"], 
/*BOWTIE*/["\u22C8"], 
/*LEFT NORMAL FACTOR SEMIDIRECT PRODUCT*/["\u22C9"], 
/*RIGHT NORMAL FACTOR SEMIDIRECT PRODUCT*/["\u22CA"], 
/*LEFT SEMIDIRECT PRODUCT*/["\u22CB"], 
/*RIGHT SEMIDIRECT PRODUCT*/["\u22CC"], 
/*REVERSED TILDE EQUALS*/["\u22CD"], 
/*PITCHFORK*/["\u22D4"], 
/*EQUAL AND PARALLEL TO*/["\u22D5"], 
/*LESS-THAN WITH DOT*/["\u22D6"], 
/*GREATER-THAN WITH DOT*/["\u22D7"], 
/*VERY MUCH LESS-THAN*/["\u22D8"], 
/*VERY MUCH GREATER-THAN*/["\u22D9"], 
/*LESS-THAN EQUAL TO OR GREATER-THAN*/["\u22DA"], 
/*GREATER-THAN EQUAL TO OR LESS-THAN*/["\u22DB"], 
/*EQUAL TO OR LESS-THAN*/["\u22DC"], 
/*EQUAL TO OR GREATER-THAN*/["\u22DD"], 
/*EQUAL TO OR PRECEDES*/["\u22DE"], 
/*EQUAL TO OR SUCCEEDS*/["\u22DF"], 
/*DOES NOT PRECEDE OR EQUAL*/["\u22E0"], 
/*DOES NOT SUCCEED OR EQUAL*/["\u22E1"], 
/*NOT SQUARE IMAGE OF OR EQUAL TO*/["\u22E2"], 
/*NOT SQUARE ORIGINAL OF OR EQUAL TO*/["\u22E3"], 
/*SQUARE IMAGE OF OR NOT EQUAL TO*/["\u22E4"], 
/*SQUARE ORIGINAL OF OR NOT EQUAL TO*/["\u22E5"], 
/*LESS-THAN BUT NOT EQUIVALENT TO*/["\u22E6"], 
/*GREATER-THAN BUT NOT EQUIVALENT TO*/["\u22E7"], 
/*PRECEDES BUT NOT EQUIVALENT TO*/["\u22E8"], 
/*SUCCEEDS BUT NOT EQUIVALENT TO*/["\u22E9"], 
/*NOT NORMAL SUBGROUP OF*/["\u22EA"], 
/*DOES NOT CONTAIN AS NORMAL SUBGROUP*/["\u22EB"], 
/*NOT NORMAL SUBGROUP OF OR EQUAL TO*/["\u22EC"], 
/*DOES NOT CONTAIN AS NORMAL SUBGROUP OR EQUAL*/["\u22ED"], 
/*VERTICAL ELLIPSIS*/["\u22EE"], 
/*MIDLINE HORIZONTAL ELLIPSIS*/["\u22EF"], 
/*UP RIGHT DIAGONAL ELLIPSIS*/["\u22F0"], 
/*DOWN RIGHT DIAGONAL ELLIPSIS*/["\u22F1"]
],
    ];
/* QuHno Mod END */


    /* ***** Define Dead Keys ************************************** */
    this.VKI_deadkey = {};

    // - Lay out each dead key set in one row of sub-arrays.  The rows
    //   below are wrapped so uppercase letters are below their
    //   lowercase equivalents.
    //
    // - The first letter in each sub-array is the letter pressed after
    //   the diacritic.  The second letter is the letter this key-combo
    //   will generate.
    //
    // - Note that if you have created a new keyboard layout and want
    //   it included in the distributed script, PLEASE TELL ME if you
    //   have added additional dead keys to the ones below.

    this.VKI_deadkey['"'] = this.VKI_deadkey['\u00a8'] = [ // Umlaut / Diaeresis / Greek Dialytika
      ["a", "\u00e4"], ["e", "\u00eb"], ["i", "\u00ef"], ["o", "\u00f6"], ["u", "\u00fc"], ["y", "\u00ff"], ["\u03b9", "\u03ca"], ["\u03c5", "\u03cb"], ["\u016B", "\u01D6"], ["\u00FA", "\u01D8"], ["\u01D4", "\u01DA"], ["\u00F9", "\u01DC"],
      ["A", "\u00c4"], ["E", "\u00cb"], ["I", "\u00cf"], ["O", "\u00d6"], ["U", "\u00dc"], ["Y", "\u0178"], ["\u0399", "\u03aa"], ["\u03a5", "\u03ab"], ["\u016A", "\u01D5"], ["\u00DA", "\u01D7"], ["\u01D3", "\u01D9"], ["\u00D9", "\u01DB"],
      ["\u304b", "\u304c"], ["\u304d", "\u304e"], ["\u304f", "\u3050"], ["\u3051", "\u3052"], ["\u3053", "\u3054"],
      ["\u305f", "\u3060"], ["\u3061", "\u3062"], ["\u3064", "\u3065"], ["\u3066", "\u3067"], ["\u3068", "\u3069"],
      ["\u3055", "\u3056"], ["\u3057", "\u3058"], ["\u3059", "\u305a"], ["\u305b", "\u305c"], ["\u305d", "\u305e"],
      ["\u306f", "\u3070"], ["\u3072", "\u3073"], ["\u3075", "\u3076"], ["\u3078", "\u3079"], ["\u307b", "\u307c"],
      ["\u30ab", "\u30ac"], ["\u30ad", "\u30ae"], ["\u30af", "\u30b0"], ["\u30b1", "\u30b2"], ["\u30b3", "\u30b4"],
      ["\u30bf", "\u30c0"], ["\u30c1", "\u30c2"], ["\u30c4", "\u30c5"], ["\u30c6", "\u30c7"], ["\u30c8", "\u30c9"],
      ["\u30b5", "\u30b6"], ["\u30b7", "\u30b8"], ["\u30b9", "\u30ba"], ["\u30bb", "\u30bc"], ["\u30bd", "\u30be"],
      ["\u30cf", "\u30d0"], ["\u30d2", "\u30d3"], ["\u30d5", "\u30d6"], ["\u30d8", "\u30d9"], ["\u30db", "\u30dc"]
    ];
    this.VKI_deadkey['~'] = [ // Tilde / Stroke
      ["a", "\u00e3"], ["l", "\u0142"], ["n", "\u00f1"], ["o", "\u00f5"],
      ["A", "\u00c3"], ["L", "\u0141"], ["N", "\u00d1"], ["O", "\u00d5"]
    ];
    this.VKI_deadkey['^'] = [ // Circumflex
      ["a", "\u00e2"], ["e", "\u00ea"], ["i", "\u00ee"], ["o", "\u00f4"], ["u", "\u00fb"], ["w", "\u0175"], ["y", "\u0177"],
      ["A", "\u00c2"], ["E", "\u00ca"], ["I", "\u00ce"], ["O", "\u00d4"], ["U", "\u00db"], ["W", "\u0174"], ["Y", "\u0176"]
    ];
    this.VKI_deadkey['\u02c7'] = [ // Baltic caron
      ["c", "\u010D"], ["d", "\u010f"], ["e", "\u011b"], ["s", "\u0161"], ["l", "\u013e"], ["n", "\u0148"], ["r", "\u0159"], ["t", "\u0165"], ["u", "\u01d4"], ["z", "\u017E"], ["\u00fc", "\u01da"],
      ["C", "\u010C"], ["D", "\u010e"], ["E", "\u011a"], ["S", "\u0160"], ["L", "\u013d"], ["N", "\u0147"], ["R", "\u0158"], ["T", "\u0164"], ["U", "\u01d3"], ["Z", "\u017D"], ["\u00dc", "\u01d9"]
    ];
    this.VKI_deadkey['\u02d8'] = [ // Romanian and Turkish breve
      ["a", "\u0103"], ["g", "\u011f"],
      ["A", "\u0102"], ["G", "\u011e"]
    ];
    this.VKI_deadkey['-'] = this.VKI_deadkey['\u00af'] = [ // Macron
      ["a", "\u0101"], ["e", "\u0113"], ["i", "\u012b"], ["o", "\u014d"], ["u", "\u016B"], ["y", "\u0233"], ["\u00fc", "\u01d6"],
      ["A", "\u0100"], ["E", "\u0112"], ["I", "\u012a"], ["O", "\u014c"], ["U", "\u016A"], ["Y", "\u0232"], ["\u00dc", "\u01d5"]
    ];
    this.VKI_deadkey['`'] = [ // Grave
      ["a", "\u00e0"], ["e", "\u00e8"], ["i", "\u00ec"], ["o", "\u00f2"], ["u", "\u00f9"], ["\u00fc", "\u01dc"],
      ["A", "\u00c0"], ["E", "\u00c8"], ["I", "\u00cc"], ["O", "\u00d2"], ["U", "\u00d9"], ["\u00dc", "\u01db"]
    ];
    this.VKI_deadkey["'"] = this.VKI_deadkey['\u00b4'] = this.VKI_deadkey['\u0384'] = [ // Acute / Greek Tonos
      ["a", "\u00e1"], ["e", "\u00e9"], ["i", "\u00ed"], ["o", "\u00f3"], ["u", "\u00fa"], ["y", "\u00fd"], ["\u03b1", "\u03ac"], ["\u03b5", "\u03ad"], ["\u03b7", "\u03ae"], ["\u03b9", "\u03af"], ["\u03bf", "\u03cc"], ["\u03c5", "\u03cd"], ["\u03c9", "\u03ce"], ["\u00fc", "\u01d8"],
      ["A", "\u00c1"], ["E", "\u00c9"], ["I", "\u00cd"], ["O", "\u00d3"], ["U", "\u00da"], ["Y", "\u00dd"], ["\u0391", "\u0386"], ["\u0395", "\u0388"], ["\u0397", "\u0389"], ["\u0399", "\u038a"], ["\u039f", "\u038c"], ["\u03a5", "\u038e"], ["\u03a9", "\u038f"], ["\u00dc", "\u01d7"]
    ];
    this.VKI_deadkey['\u02dd'] = [ // Hungarian Double Acute Accent
      ["o", "\u0151"], ["u", "\u0171"],
      ["O", "\u0150"], ["U", "\u0170"]
    ];
    this.VKI_deadkey['\u0385'] = [ // Greek Dialytika + Tonos
      ["\u03b9", "\u0390"], ["\u03c5", "\u03b0"]
    ];
    this.VKI_deadkey['\u00b0'] = this.VKI_deadkey['\u00ba'] = [ // Ring
      ["a", "\u00e5"], ["u", "\u016f"],
      ["A", "\u00c5"], ["U", "\u016e"]
    ];
    this.VKI_deadkey['\u02DB'] = [ // Ogonek
      ["a", "\u0106"], ["e", "\u0119"], ["i", "\u012f"], ["o", "\u01eb"], ["u", "\u0173"], ["y", "\u0177"],
      ["A", "\u0105"], ["E", "\u0118"], ["I", "\u012e"], ["O", "\u01ea"], ["U", "\u0172"], ["Y", "\u0176"]
    ];
    this.VKI_deadkey['\u02D9'] = [ // Dot-above
      ["c", "\u010B"], ["e", "\u0117"], ["g", "\u0121"], ["z", "\u017C"],
      ["C", "\u010A"], ["E", "\u0116"], ["G", "\u0120"], ["Z", "\u017B"]
    ];
    this.VKI_deadkey['\u00B8'] = this.VKI_deadkey['\u201a'] = [ // Cedilla
      ["c", "\u00e7"], ["s", "\u015F"],
      ["C", "\u00c7"], ["S", "\u015E"]
    ];
    this.VKI_deadkey[','] = [ // Comma
      ["s", (this.VKI_isIElt8) ? "\u015F" : "\u0219"], ["t", (this.VKI_isIElt8) ? "\u0163" : "\u021B"],
      ["S", (this.VKI_isIElt8) ? "\u015E" : "\u0218"], ["T", (this.VKI_isIElt8) ? "\u0162" : "\u021A"]
    ];
    this.VKI_deadkey['\u3002'] = [ // Hiragana/Katakana Point
      ["\u306f", "\u3071"], ["\u3072", "\u3074"], ["\u3075", "\u3077"], ["\u3078", "\u307a"], ["\u307b", "\u307d"],
      ["\u30cf", "\u30d1"], ["\u30d2", "\u30d4"], ["\u30d5", "\u30d7"], ["\u30d8", "\u30da"], ["\u30db", "\u30dd"]
    ];
	
/* QuHno Mod START */
	this.VKI_deadkey['\u00AC'] = [ /* Mathematical NOT. Combined several different NOT notations, makes no sense with Letters. Problem with Turkish KB?*/
      ["=", "\u2260"], ["<","\u226E"], [">","\u226F"], /*<=*/["\u2264","\u2270"], /*>=*/["\u2265","\u2271"], /*pecedes*/["\u227A","\u2280"], /*succedes*/["\u227B","\u2281"], ["\u2261","\u2263"]
    ];
	this.VKI_deadkey['/'] = this.VKI_deadkey['\u2215'] = this.VKI_deadkey['\u2044'] = this.VKI_deadkey['\u0338'] = [ /* Combining Long Solidus Overlay, no-whitespace slash (used for "not" in Math). */
      ["=", "\u2260"], ["<","\u226E"], [">","\u226F"], /*<=*/["\u2264","\u2270"], /*>=*/["\u2265","\u2271"], /*pecedes*/["\u227A","\u2280"], /*succedes*/["\u227B","\u2281"], 
	  ["O","\u00D8"]
    ];
	this.VKI_deadkey['/'] = this.VKI_deadkey['\u0337'] = [ /* Combining Short Solidus Overlay, no-whitespace slash. */
      ["o","\u00F8"]
    ];
/* QuHno Mod END */


    /* ***** Define Symbols **************************************** */
    this.VKI_symbol = {
      '\u200c': "ZW\r\nNJ", '\u200d': "ZW\r\nJ"
    };



    /* ****************************************************************
     * Attach the keyboard to an element
     *
     */
    this.VKI_attachKeyboard = VKI_attach = function(elem) {
        if (elem.VKI_attached) return false;
        if (this.VKI_imageURI) {
            var keybut = document.createElement('img');
            keybut.src = this.VKI_imageURI;
            keybut.alt = this.VKI_i18n['00'];
            keybut.className = "keyboardInputInitiator";
            keybut.title = this.VKI_i18n['01'];
            keybut.elem = elem;
            keybut.onclick = function() { self.VKI_show(this.elem); };
            elem.parentNode.insertBefore(keybut, (elem.dir == "rtl") ? elem : elem.nextSibling);
        } else elem.onfocus = function() { if (self.VKI_target != this) self.VKI_show(this); };
        elem.VKI_attached = true;
        if (this.VKI_isIE) {
            elem.onclick = elem.onselect = elem.onkeyup = function(e) {
                if ((e || event).type != "keyup" || !this.readOnly)
                    this.range = document.selection.createRange();
            };
        }
    };


    /* ***** Find tagged input & textarea elements ***************** */
    var inputElems = [
      document.getElementsByTagName('input'),
      document.getElementsByTagName('textarea')
    ];
    for (var x = 0, elem; elem = inputElems[x++];)
      for (var y = 0, ex; ex = elem[y++];)
        if ((ex.nodeName == "TEXTAREA" || ex.type == "text" || ex.type == "password") && ex.className.indexOf("keyboardInput") > -1)
          this.VKI_attachKeyboard(ex);


    /* ***** Build the keyboard interface ************************** */
    this.VKI_keyboard = document.createElement('table');
    this.VKI_keyboard.id = "keyboardInputMaster";
    this.VKI_keyboard.dir = "ltr";
    this.VKI_keyboard.cellSpacing = this.VKI_keyboard.border = "0";

    var thead = document.createElement('thead');
      var tr = document.createElement('tr');
        var th = document.createElement('th');
          var abbr = document.createElement('abbr');
              abbr.title = this.VKI_i18n['00'];
              abbr.appendChild(document.createTextNode('VKI'));
            th.appendChild(abbr);

          var kblist = document.createElement('select');
              kblist.title = this.VKI_i18n['02'];
            for (ktype in this.VKI_layout) {
              if (typeof this.VKI_layout[ktype] == "object") {
                if (!this.VKI_layout[ktype].lang) this.VKI_layout[ktype].lang = [];
                var opt = document.createElement('option');
                    opt.value = ktype;
                    opt.appendChild(document.createTextNode(ktype));
                  kblist.appendChild(opt);
              }
            }
            if (kblist.options.length) {
                kblist.value = this.VKI_kt;
                kblist.onchange = function() {
                  self.VKI_kts = self.VKI_kt = this.value;
                  self.VKI_buildKeys();
                  self.VKI_position(true);
                };
              th.appendChild(kblist);
            }

          if (this.VKI_sizeAdj) {
            this.VKI_size = Math.min(1, Math.max(1, this.VKI_size));
            var kbsize = document.createElement('select');
                kbsize.title = this.VKI_i18n['10'];
              for (var x = 1; x <= 5; x++) {
                var opt = document.createElement('option');
                    opt.value = x;
                    opt.appendChild(document.createTextNode(x));
                  kbsize.appendChild(opt);
              } kbsize.value = this.VKI_size;
                kbsize.change = function() {
                  self.VKI_size = this.value;
                  self.VKI_keyboard.className = self.VKI_keyboard.className.replace(/ ?keyboardInputSize\d ?/, "");
                  if (this.value != 2) self.VKI_keyboard.className += " keyboardInputSize" + this.value;
                  self.VKI_position(true);
                };
                kbsize.onchange = kbsize.change;
            th.appendChild(kbsize);
          }

            var label = document.createElement('label');
              var checkbox = document.createElement('input');
                  checkbox.type = "checkbox";
                  checkbox.title = this.VKI_i18n['03'] + ": " + ((this.VKI_deadkeysOn) ? this.VKI_i18n['04'] : this.VKI_i18n['05']);
                  checkbox.defaultChecked = this.VKI_deadkeysOn;
                  checkbox.onclick = function() {
                    self.VKI_deadkeysOn = this.checked;
                    this.title = self.VKI_i18n['03'] + ": " + ((this.checked) ? self.VKI_i18n['04'] : self.VKI_i18n['05']);
                    self.VKI_modify("");
                    return true;
                  };
                label.appendChild(this.VKI_deadkeysElem = checkbox);
                  checkbox.checked = this.VKI_deadkeysOn;
            th.appendChild(label);
          tr.appendChild(th);

        var td = document.createElement('td');
          var clearer = document.createElement('span');
              clearer.id = "keyboardInputClear";
              clearer.appendChild(document.createTextNode(this.VKI_i18n['07']));
              clearer.title = this.VKI_i18n['08'];
              clearer.onmousedown = function() { this.className = "pressed"; };
              clearer.onmouseup = function() { this.className = ""; };
              clearer.onclick = function() {
                self.VKI_target.value = "";
                self.VKI_target.focus();
                return false;
              };
            td.appendChild(clearer);

          var closer = document.createElement('strong');
              closer.id = "keyboardInputClose";
              closer.appendChild(document.createTextNode('X'));
              closer.title = this.VKI_i18n['06'];
              closer.onmousedown = function() { this.className = "pressed"; };
              closer.onmouseup = function() { this.className = ""; };
              closer.onclick = function() { self.VKI_close(); };
            td.appendChild(closer);

          tr.appendChild(td);
        thead.appendChild(tr);
    this.VKI_keyboard.appendChild(thead);

    var tbody = document.createElement('tbody');
      var tr = document.createElement('tr');
        var td = document.createElement('td');
            td.colSpan = "2";
          var div = document.createElement('div');
              div.id = "keyboardInputLayout";
            td.appendChild(div);
          if (this.VKI_showVersion) {
            var div = document.createElement('div');
              var ver = document.createElement('var');
                  ver.title = this.VKI_i18n['09'] + " " + this.VKI_version;
                  ver.appendChild(document.createTextNode("v" + this.VKI_version));
                div.appendChild(ver);
              td.appendChild(div);
          }
          tr.appendChild(td);
        tbody.appendChild(tr);
    this.VKI_keyboard.appendChild(tbody);

    if (this.VKI_isIE6) {
      this.VKI_iframe = document.createElement('iframe');
      this.VKI_iframe.style.position = "absolute";
      this.VKI_iframe.style.border = "0px none";
      this.VKI_iframe.style.filter = "mask()";
      this.VKI_iframe.style.zIndex = "999999";
      this.VKI_iframe.src = this.VKI_imageURI;
    }


    /* ****************************************************************
     * Build or rebuild the keyboard keys
     *
     */
    this.VKI_buildKeys = function() {
      this.VKI_shift = this.VKI_shiftlock = this.VKI_altgr = this.VKI_altgrlock = this.VKI_dead = false;
      this.VKI_deadkeysOn = (this.VKI_layout[this.VKI_kt].DDK) ? false : this.VKI_keyboard.getElementsByTagName('label')[0].getElementsByTagName('input')[0].checked;

      var container = this.VKI_keyboard.tBodies[0].getElementsByTagName('div')[0];
      while (container.firstChild) container.removeChild(container.firstChild);

      for (var x = 0, hasDeadKey = false, lyt; lyt = this.VKI_layout[this.VKI_kt][x++];) {
        var table = document.createElement('table');
            table.cellSpacing = table.border = "0";
        if (lyt.length <= this.VKI_keyCenter) table.className = "keyboardInputCenter";
          var tbody = document.createElement('tbody');
            var tr = document.createElement('tr');
            for (var y = 0, lkey; lkey = lyt[y++];) {
              var td = document.createElement('td');
                if (this.VKI_symbol[lkey[0]]) {
                  var span = document.createElement('span');
                      span.className = lkey[0];
                      span.appendChild(document.createTextNode(this.VKI_symbol[lkey[0]]));
                    td.appendChild(span);
                } else td.appendChild(document.createTextNode(lkey[0] || "\xa0"));

                var className = [];
                if (this.VKI_deadkeysOn)
                  for (key in this.VKI_deadkey)
                    if (key === lkey[0]) { className.push("alive"); break; }
                if (lyt.length > this.VKI_keyCenter && y == lyt.length) className.push("last");
                if (lkey[0] == " ") className.push("space");
                  td.className = className.join(" ");

                  td.VKI_clickless = 0;
                  if (!td.click) {
                    td.click = function() {
                      var evt = this.ownerDocument.createEvent('MouseEvents');
                      evt.initMouseEvent('click', true, true, this.ownerDocument.defaultView, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
                      this.dispatchEvent(evt);
                    };
                  }
                  td.onmouseover = function() {
                    if (self.VKI_clickless) {
                      var _self = this;
                      clearTimeout(this.VKI_clickless);
                      this.VKI_clickless = setTimeout(function() { _self.click(); }, self.VKI_clickless);
                    }
                    if ((this.firstChild.nodeValue || this.firstChild.className) != "\xa0") this.className += " hover";
                  };
                  td.onmouseout = function() {
                    if (self.VKI_clickless) clearTimeout(this.VKI_clickless);
                    this.className = this.className.replace(/ ?(hover|pressed)/g, "");
                  };
                  td.onmousedown = function() {
                    if (self.VKI_clickless) clearTimeout(this.VKI_clickless);
                    if ((this.firstChild.nodeValue || this.firstChild.className) != "\xa0") this.className += " pressed";
                  };
                  td.onmouseup = function() {
                    if (self.VKI_clickless) clearTimeout(this.VKI_clickless);
                    this.className = this.className.replace(/ ?pressed/g, "");
                  };
                  td.ondblclick = function() { return false; };

                switch (lkey[1]) {
                  case "Caps": case "Shift":
                  case "Alt": case "AltGr": case "AltLk":
                    td.onclick = (function(type) { return function() { self.VKI_modify(type); return false; }; })(lkey[1]);
                    break;
                  case "Tab":
                    td.onclick = function() { self.VKI_insert("\t"); return false; };
                    break;
                  case "Bksp":
                    td.onclick = function() {
                      self.VKI_target.focus();
                      if (self.VKI_target.setSelectionRange && !self.VKI_target.readOnly) {
                        var rng = [self.VKI_target.selectionStart, self.VKI_target.selectionEnd];
                        if (rng[0] < rng[1]) rng[0]++;
                        self.VKI_target.value = self.VKI_target.value.substr(0, rng[0] - 1) + self.VKI_target.value.substr(rng[1]);
                        self.VKI_target.setSelectionRange(rng[0] - 1, rng[0] - 1);
                      } else if (self.VKI_target.createTextRange && !self.VKI_target.readOnly) {
                        try {
                          self.VKI_target.range.select();
                        } catch(e) { self.VKI_target.range = document.selection.createRange(); }
                        if (!self.VKI_target.range.text.length) self.VKI_target.range.moveStart('character', -1);
                        self.VKI_target.range.text = "";
                      } else self.VKI_target.value = self.VKI_target.value.substr(0, self.VKI_target.value.length - 1);
                      if (self.VKI_shift) self.VKI_modify("Shift");
                      if (self.VKI_altgr) self.VKI_modify("AltGr");
                      self.VKI_target.focus();
                      return true;
                    };
                    break;
                  case "Enter":
                    td.onclick = function() {
                      if (self.VKI_target.nodeName != "TEXTAREA") {
                        self.VKI_close();
                        this.className = this.className.replace(/ ?(hover|pressed)/g, "");
                      } else self.VKI_insert("\n");
                      return true;
                    };
                    break;
                  default:
                    td.onclick = function() {
                      var character = this.firstChild.nodeValue || this.firstChild.className;
                      if (self.VKI_deadkeysOn && self.VKI_dead) {
                        if (self.VKI_dead != character) {
                          for (key in self.VKI_deadkey) {
                            if (key == self.VKI_dead) {
                              if (character != " ") {
                                for (var z = 0, rezzed = false, dk; dk = self.VKI_deadkey[key][z++];) {
                                  if (dk[0] == character) {
                                    self.VKI_insert(dk[1]);
                                    rezzed = true;
                                    break;
                                  }
                                }
                              } else {
                                self.VKI_insert(self.VKI_dead);
                                rezzed = true;
                              } break;
                            }
                          }
                        } else rezzed = true;
                      } self.VKI_dead = false;

                      if (!rezzed && character != "\xa0") {
                        if (self.VKI_deadkeysOn) {
                          for (key in self.VKI_deadkey) {
                            if (key == character) {
                              self.VKI_dead = key;
                              this.className += " dead";
                              if (self.VKI_shift) self.VKI_modify("Shift");
                              if (self.VKI_altgr) self.VKI_modify("AltGr");
                              break;
                            }
                          }
                          if (!self.VKI_dead) self.VKI_insert(character);
                        } else self.VKI_insert(character);
                      }

                      self.VKI_modify("");
                      if (self.VKI_isOpera) {
                        this.style.width = "50px";
                        var foo = this.offsetWidth;
                        this.style.width = "";
                      }
                      return false;
                    };

                }
                tr.appendChild(td);
              tbody.appendChild(tr);
            table.appendChild(tbody);

            for (var z = 0; z < 4; z++)
              if (this.VKI_deadkey[lkey[z] = lkey[z] || "\xa0"]) hasDeadKey = true;
        }
        container.appendChild(table);
      }
      this.VKI_deadkeysElem.style.display = (!this.VKI_layout[this.VKI_kt].DDK && hasDeadKey) ? "inline" : "none";
    };

    this.VKI_buildKeys();
    VKI_disableSelection(this.VKI_keyboard);


    /* ****************************************************************
     * Controls modifier keys
     *
     */
    this.VKI_modify = function(type) {
      switch (type) {
        case "Alt":
        case "AltGr": this.VKI_altgr = !this.VKI_altgr; break;
        /* case "AltLk": this.VKI_altgrlock = !this.VKI_altgrlock; break; */
		case "AltLk": this.VKI_altgr = 0; this.VKI_altgrlock = !this.VKI_altgrlock; break;
        /* case "Caps": this.VKI_shiftlock = !this.VKI_shiftlock; break; */
		case "Caps": this.VKI_shift = 0; this.VKI_shiftlock = !this.VKI_shiftlock; break;
        case "Shift": this.VKI_shift = !this.VKI_shift; break;
      } var vchar = 0;
      if (!this.VKI_shift != !this.VKI_shiftlock) vchar += 1;
      if (!this.VKI_altgr != !this.VKI_altgrlock) vchar += 2;

      var tables = this.VKI_keyboard.getElementsByTagName('table');
      for (var x = 0; x < tables.length; x++) {
        var tds = tables[x].getElementsByTagName('td');
        for (var y = 0; y < tds.length; y++) {
          var className = [], lkey = this.VKI_layout[this.VKI_kt][x][y];

          if (tds[y].className.indexOf('hover') > -1) className.push("hover");

          switch (lkey[1]) {
            case "Alt":
            case "AltGr":
              if (this.VKI_altgr) className.push("dead");
              break;
            case "AltLk":
              if (this.VKI_altgrlock) className.push("dead");
              break;
            case "Shift":
              if (this.VKI_shift) className.push("dead");
              break;
            case "Caps":
              if (this.VKI_shiftlock) className.push("dead");
              break;
            case "Tab": case "Enter": case "Bksp": break;
            default:
              if (type) {
                tds[y].removeChild(tds[y].firstChild);
                if (this.VKI_symbol[lkey[vchar]]) {
                  var span = document.createElement('span');
                      span.className = lkey[vchar];
                      span.appendChild(document.createTextNode(this.VKI_symbol[lkey[vchar]]));
                    tds[y].appendChild(span);
                } else tds[y].appendChild(document.createTextNode(lkey[vchar]));
              }
              if (this.VKI_deadkeysOn) {
                var character = tds[y].firstChild.nodeValue || tds[y].firstChild.className;
                if (this.VKI_dead) {
                  if (character == this.VKI_dead) className.push("dead");
                  for (var z = 0; z < this.VKI_deadkey[this.VKI_dead].length; z++) {
                    if (character == this.VKI_deadkey[this.VKI_dead][z][0]) {
                      className.push("target");
                      break;
                    }
                  }
                }
                for (key in this.VKI_deadkey)
                  if (key === character) { className.push("alive"); break; }
              }
          }

          if (y == tds.length - 1 && tds.length > this.VKI_keyCenter) className.push("last");
          if (lkey[0] == " ") className.push("space");
          tds[y].className = className.join(" ");
        }
      }
    };


    /* ****************************************************************
     * Insert text at the cursor
     *
     */
    this.VKI_insert = function(text) {
      this.VKI_target.focus();
      if (this.VKI_target.maxLength) this.VKI_target.maxlength = this.VKI_target.maxLength;
      if (typeof this.VKI_target.maxlength == "undefined" ||
          this.VKI_target.maxlength < 0 ||
          this.VKI_target.value.length < this.VKI_target.maxlength) {
        if (this.VKI_target.setSelectionRange && !this.VKI_target.readOnly) {
          var rng = [this.VKI_target.selectionStart, this.VKI_target.selectionEnd];
          this.VKI_target.value = this.VKI_target.value.substr(0, rng[0]) + text + this.VKI_target.value.substr(rng[1]);
          if (text == "\n" && window.opera) rng[0]++;
          this.VKI_target.setSelectionRange(rng[0] + text.length, rng[0] + text.length);
        } else if (this.VKI_target.createTextRange && !this.VKI_target.readOnly) {
          try {
            this.VKI_target.range.select();
          } catch(e) { this.VKI_target.range = document.selection.createRange(); }
          this.VKI_target.range.text = text;
          this.VKI_target.range.collapse(true);
          this.VKI_target.range.select();
        } else this.VKI_target.value += text;
        if (this.VKI_shift) this.VKI_modify("Shift");
        if (this.VKI_altgr) this.VKI_modify("AltGr");
        this.VKI_target.focus();
      } else if (this.VKI_target.createTextRange && this.VKI_target.range)
        this.VKI_target.range.select();
    };


    /* ****************************************************************
     * Show the keyboard interface
     *
     */
    this.VKI_show = function(elem) {
      if (!this.VKI_target) {
        this.VKI_target = elem;
        if (this.VKI_langAdapt && this.VKI_target.lang) {
          var chg = false, sub = [];
          for (ktype in this.VKI_layout) {
            if (typeof this.VKI_layout[ktype] == "object") {
              for (var x = 0; x < this.VKI_layout[ktype].lang.length; x++) {
                if (this.VKI_layout[ktype].lang[x].toLowerCase() == this.VKI_target.lang.toLowerCase()) {
                  chg = kblist.value = this.VKI_kt = ktype;
                  break;
                } else if (this.VKI_layout[ktype].lang[x].toLowerCase().indexOf(this.VKI_target.lang.toLowerCase()) == 0)
                  sub.push([this.VKI_layout[ktype].lang[x], ktype]);
              }
            } if (chg) break;
          } if (sub.length) {
            sub.sort(function (a, b) { return a[0].length - b[0].length; });
            chg = kblist.value = this.VKI_kt = sub[0][1];
          } if (chg) this.VKI_buildKeys();
        }
        if (this.VKI_isIE) {
          if (!this.VKI_target.range) {
            this.VKI_target.range = this.VKI_target.createTextRange();
            this.VKI_target.range.moveStart('character', this.VKI_target.value.length);
          } this.VKI_target.range.select();
        }
        try { this.VKI_keyboard.parentNode.removeChild(this.VKI_keyboard); } catch (e) {}
        if (this.VKI_clearPasswords && this.VKI_target.type == "password") this.VKI_target.value = "";

        var elem = this.VKI_target;
        this.VKI_target.keyboardPosition = "absolute";
        do {
          if (VKI_getStyle(elem, "position") == "fixed") {
            this.VKI_target.keyboardPosition = "fixed";
            break;
          }
        } while (elem = elem.offsetParent);

        if (this.VKI_isIE6) document.body.appendChild(this.VKI_iframe);
        document.body.appendChild(this.VKI_keyboard);
        this.VKI_keyboard.style.position = this.VKI_target.keyboardPosition;
        if (this.VKI_isOpera) {
          if (this.VKI_sizeAdj) kbsize.value = this.VKI_size;
          kblist.value = this.VKI_kt;
        }

        this.VKI_position(true);
        if (self.VKI_isMoz || self.VKI_isWebKit) this.VKI_position(true);
        this.VKI_target.focus();
      } else this.VKI_close();
    };


    /* ****************************************************************
     * Position the keyboard
     *
     */
    this.VKI_position = function(force) {
      if (self.VKI_target) {
        var kPos = VKI_findPos(self.VKI_keyboard), wDim = VKI_innerDimensions(), sDis = VKI_scrollDist();
        var place = false, fudge = self.VKI_target.offsetHeight + 3;
        if (force !== true) {
          if (kPos[1] + self.VKI_keyboard.offsetHeight - sDis[1] - wDim[1] > 0) {
            place = true;
            fudge = -self.VKI_keyboard.offsetHeight - 3;
          } else if (kPos[1] - sDis[1] < 0) place = true;
        }
        if (place || force === true) {
          var iPos = VKI_findPos(self.VKI_target);
          self.VKI_keyboard.style.top = iPos[1] - ((self.VKI_target.keyboardPosition == "fixed" && !self.VKI_isIE && !self.VKI_isMoz) ? sDis[1] : 0) + fudge + "px";
          self.VKI_keyboard.style.left = Math.max(10, Math.min(wDim[0] - self.VKI_keyboard.offsetWidth - 25, iPos[0])) + "px";
          if (self.VKI_isIE6) {
            self.VKI_iframe.style.width = self.VKI_keyboard.offsetWidth + "px";
            self.VKI_iframe.style.height = self.VKI_keyboard.offsetHeight + "px";
            self.VKI_iframe.style.top = self.VKI_keyboard.style.top;
            self.VKI_iframe.style.left = self.VKI_keyboard.style.left;
          }
        }
        if (force === true) self.VKI_position();
      }
    };


    if (window.addEventListener) {
      window.addEventListener('resize', this.VKI_position, false);
      window.addEventListener('scroll', this.VKI_position, false);
    } else if (window.attachEvent) {
      window.attachEvent('onresize', this.VKI_position);
      window.attachEvent('onscroll', this.VKI_position);
    }
    if (this.VKI_sizeAdj) kbsize.change();


    /* ****************************************************************
     * Close the keyboard interface
     *
     */
    this.VKI_close = VKI_close = function() {
      if (this.VKI_target) {
        try {
          this.VKI_keyboard.parentNode.removeChild(this.VKI_keyboard);
          if (this.VKI_isIE6) this.VKI_iframe.parentNode.removeChild(this.VKI_iframe);
        } catch (e) {}
        if (this.VKI_kt != this.VKI_kts) {
          kblist.value = this.VKI_kt = this.VKI_kts;
          this.VKI_buildKeys();
        }
        this.VKI_target.focus();
        if (this.VKI_isIE) {
          setTimeout(function() { self.VKI_target = false; }, 0);
        } else this.VKI_target = false;
      }
    };
  }

  function VKI_findPos(obj) {
    var curleft = curtop = 0;
    do {
      curleft += obj.offsetLeft;
      curtop += obj.offsetTop;
    } while (obj = obj.offsetParent);
    return [curleft, curtop];
  }

  function VKI_innerDimensions() {
    if (self.innerHeight) {
      return [self.innerWidth, self.innerHeight];
    } else if (document.documentElement && document.documentElement.clientHeight) {
      return [document.documentElement.clientWidth, document.documentElement.clientHeight];
    } else if (document.body)
      return [document.body.clientWidth, document.body.clientHeight];
    return [0, 0];
  }

  function VKI_scrollDist() {
    var html = document.getElementsByTagName('html')[0];
    if (html.scrollTop && document.documentElement.scrollTop) {
      return [html.scrollLeft, html.scrollTop];
    } else if (html.scrollTop || document.documentElement.scrollTop) {
      return [html.scrollLeft + document.documentElement.scrollLeft, html.scrollTop + document.documentElement.scrollTop];
    } else if (document.body.scrollTop)
      return [document.body.scrollLeft, document.body.scrollTop];
    return [0, 0];
  }

  function VKI_getStyle(obj, styleProp) {
    if (obj.currentStyle) {
      var y = obj.currentStyle[styleProp];
    } else if (window.getComputedStyle)
      var y = window.getComputedStyle(obj, null)[styleProp];
    return y;
  }

  function VKI_disableSelection(elem) {
    elem.onselectstart = function() { return false; };
    elem.unselectable = "on";
    elem.style.MozUserSelect = "none";
    elem.style.cursor = "default";
    if (window.opera) elem.onmousedown = function() { return false; };
  }


  /* ***** Attach this script to the onload event ****************** */
  if (window.addEventListener) {
    window.addEventListener('load', VKI_buildKeyboardInputs, false);
  } else if (window.attachEvent)
    window.attachEvent('onload', VKI_buildKeyboardInputs);