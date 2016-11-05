- CSS1 (https://www.w3.org/TR/REC-CSS1/)
	- Containment
		- External CSS file
		- Inline style tag
		- Inline style attribute
		- Referenced stylesheet
		- Import ("@import")
	- Selectors
		- Grouping (",") - 0.1
		- Class selector - 0.1
		- ID selector - 0.1
		- Inheritance selector (" ") - 0.1
		- Pseudo classes
			- :link
			- :visited
			- :active
		- Pseudo elements
			- :first-line
			- :first-letter
	- Declarations
		- Property grouping ("bold 12pt/14pt helvetica")
		- Important declarations ("!important")
		- Specificity order
	- Replaced content elements ("img")
	- Properties
		- Fonts
			- font-family - X
			- font-style - X
			- font-variant - X
			- font-weight - X
			- font-size - X
			- font - X
		- Color and background
			- color = <color>
			- background-color = <color>
			- background-image - X
			- backround-repeat - X
			- background-attachment - X
			- background-position - X
			- background - <background-color> only
		- Text
			- word-spacing =  normal | <length>
			- letter-spacing = normal | <length> 
			- text-decoration = none | blink
				-  underline | overline | line-through - X
			- vertical-align = baseline | sub | super | top | text-top | middle | bottom | text-bottom | <percentage> 
			- text-transform = capitalize | uppercase | lowercase | none
			- text-align = left | right | center | justify
			- text-indent = <length> | <percentage>
			- line-height = normal | <number> | <length> | <percentage>
		- Box
			- margin-top = <length> | <percentage> | auto
			- margin-right = <length> | <percentage> | auto
			- margin-bottom = <length> | <percentage> | auto
			- margin-left = <length> | <percentage> | auto
			- margin = [ <length> | <percentage> | auto ]{1,4}
			- padding-top = <length> | <percentage> | auto
			- padding-right = <length> | <percentage> | auto
			- padding-bottom = <length> | <percentage> | auto
			- padding-left = <length> | <percentage> | auto
			- padding = [ <length> | <percentage> | auto ]{1,4}
			- border-top-width = thin | medium | thick | <length>
			- border-right-width = thin | medium | thick | <length>
			- border-bottom-width = thin | medium | thick | <length>
			- border-left-width = thin | medium | thick | <length>
			- border-width = [ thin | medium | thick | <length> ]{1,4}
			- border-top-color = <color>
			- border-right-color = <color>
			- border-bottom-color = <color>
			- border-left-color = <color>
			- border-color = <color>{1,4}
			- border-top-style = none | dotted | dashed | solid | double | groove | ridge | inset | outset
			- border-right-style = none | dotted | dashed | solid | double | groove | ridge | inset | outset
			- border-bottom-style = none | dotted | dashed | solid | double | groove | ridge | inset | outset
			- border-left-style = none | dotted | dashed | solid | double | groove | ridge | inset | outset
			- border-style = [ none | dotted | dashed | solid | double | groove | ridge | inset | outset ]{1,4}
			- border-top = <border-top-width> || <border-style> || <color>
			- border-right = <border-top-width> || <border-style> || <color>
			- border-bottom = <border-top-width> || <border-style> || <color>
			- border-left = <border-top-width> || <border-style> || <color>
			- border = <border-top-width> || <border-style> || <color>
			- width = <length> | <percentage> | auto 
			- height = <length> | auto 
			- float = left | right | none
			- clear = none | left | right | both
		- Classification
			- display = block | inline | list-item | none
			- white-space = normal | pre | nowrap
			- list-style-type = disc | circle | square | decimal | lower-roman | upper-roman | lower-alpha | upper-alpha | none
			- list-style-image = X
			- list-style-position = inside | outside
			- list-style = [disc | circle | square | decimal | lower-roman | upper-roman | lower-alpha | upper-alpha | none] || [inside | outside] 
				- <url> - X
	- Units
		- Length
			- em
			- ex
			- in
			- cm
			- mm
			- pt
			- pc
		- Percentage
		- Color
			- Keyword colors
			- Hash RGB
			- Functional RGB
				- 0-255
				- Percentage
		- URL - X
CSS 2.2 (https://www.w3.org/TR/CSS22)
	- Selectors
		- * - 0.1
		- Child of (">") - 0.1
		- Adjacent sibling ("+")
		- Attribute set ("[x]")
		- Attribute exact value ('[x="y"]')
		- Attribute has value ('[x~="y"]')
		- Attribute has value in hyphenated list ('[x|="y"]')
		- Pseudo classes
			- :first-child
			- :lang(c)
		- Pseudo elements
			- :before
			- :after
		- Page
			- :left -X
			- :right - X
			- :first
	- Property computation (specified -> computed -> used -> actual)
	- "inherit"
	- Media types
		- Media dependent rule blocks
		- Media dependent referenced stylesheets
		- Types
			- all
			- braille - X
			- embossed - X
			- print
			- handheld
			- projection
			- screen
			- speech - X
			- tty
			- tv
	- Paging
		- @page
	- Properties
		- Visual formatting model
			- display: inline | block | list-item | inline-block | table | inline-table | table-row-group | table-header-group | table-footer-group | table-row | table-column-group | table-column | table-cell | table-caption | none
			- position: static | relative | absolute | fixed
			- top: <length> | <percentage> | auto
			- right: <length> | <percentage> | auto
			- bottom: <length> | <percentage> | auto
			- left: <length> | <percentage> | auto
			- z-index: auto | <integer>
			- direction: ltr | rtl
			- direction-bidi: normal | embed | bidi-override
			- min-width: <length> | <percentage>
			- max-width: <length> | <percentage> | none
			- min-height: <length> | <percentage>
			- max-height: <length> | <percentage> | none
			- visibility: visible | hidden | collapse
		- Visual effects
			- overflow: visible | hidden | scroll | auto
			- clip: <shape> | auto
		- Generators
			- content: normal | none | [ <string> | <uri> | <counter> | attr(<identifier>) | open-quote | close-quote | no-open-quote | no-close-quote ]+
			- quotes: [<string> <string>]+ | none
			- counter-reset: [ <identifier> <integer>? ]+ | none
			- counter-increment: [ <identifier> <integer>? ]+ | none
			- list-style-type: disc | circle | square | decimal | decimal-leading-zero | lower-roman | upper-roman | lower-greek | lower-latin | upper-latin | armenian | georgian | lower-alpha | upper-alpha | none
		- Paging
			- page-break-before: auto | always | avoid | left | right
			- page-break-after: auto | always | avoid | left | right
			- page-break-inside: avoid | auto
			- orphans: <integer>
			- widows: <integer>
		- Colors
			- transparent
		- Fonts
			- Generic font families - X
		- Tables
			- caption-side: top | bottom
			- table-layout: auto | fixed
			- border-collapse: collapse | separate
			- border-spacing: <length> <length>?
			- empty-cells: show | hide
		- User interface
			- System colors
			- cursor - X
			- outline - X
			- outline-style - X
			- outline-width - X
			- outline-color - X
		- Aural - X
























