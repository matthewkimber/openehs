//----------------------------------------------------------------------------
//
//  $Id: PreviewAndPrintLabel.js 11419 2010-04-07 21:18:22Z vbuzuev $ 
//
// Project -------------------------------------------------------------------
//
//  DYMO Label Framework
//
// Content -------------------------------------------------------------------
//
//  DYMO Label Framework JavaScript Library Samples: Print label
//
//----------------------------------------------------------------------------
//
//  Copyright (c), 2010, Sanford, L.P. All Rights Reserved.
//
//----------------------------------------------------------------------------


(function () {
    // called when the document completly loaded
    function onload() {
        var printButton = document.getElementById('printButton');

        // prints the label
        printButton.onclick = function () {
            try {
                // open label
                var labelXml = '<DieCutLabel Version="8.0" Units="twips">\
	                                <PaperOrientation>Landscape</PaperOrientation>\
	                                <Id>Address</Id>\
	                                <PaperName>30252 Address</PaperName>\
	                                <DrawCommands>\
		                                <RoundRectangle X="0" Y="0" Width="1581" Height="5040" Rx="270" Ry="270" />\
	                                </DrawCommands>\
	                                <ObjectInfo>\
		                                <TextObject>\
			                                <Name>TEXT_BOTTOM_LEFT</Name>\
			                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                                <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                                <LinkedObjectName></LinkedObjectName>\
			                                <Rotation>Rotation0</Rotation>\
			                                <IsMirrored>False</IsMirrored>\
			                                <IsVariable>False</IsVariable>\
			                                <HorizontalAlignment>Center</HorizontalAlignment>\
			                                <VerticalAlignment>Top</VerticalAlignment>\
			                                <TextFitMode>ShrinkToFit</TextFitMode>\
			                                <UseFullFontHeight>True</UseFullFontHeight>\
			                                <Verticalized>False</Verticalized>\
			                                <StyledText />\
		                                </TextObject>\
		                                <Bounds X="361" Y="1269.36413574219" Width="1963.47082519531" Height="216" />\
	                                </ObjectInfo>\
	                                <ObjectInfo>\
		                                <BarcodeObject>\
			                                <Name>BC_BOTTOM_LEFT</Name>\
			                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                                <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                                <LinkedObjectName></LinkedObjectName>\
			                                <Rotation>Rotation0</Rotation>\
			                                <IsMirrored>False</IsMirrored>\
			                                <IsVariable>True</IsVariable>\
			                                <Text>9999999</Text>\
			                                <Type>Code128Auto</Type>\
			                                <Size>Medium</Size>\
			                                <TextPosition>None</TextPosition>\
			                                <TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			                                <CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			                                <TextEmbedding>None</TextEmbedding>\
			                                <ECLevel>0</ECLevel>\
			                                <HorizontalAlignment>Center</HorizontalAlignment>\
			                                <QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
		                                </BarcodeObject>\
		                                <Bounds X="331" Y="863.822326660156" Width="2246.07104492188" Height="360" />\
	                                </ObjectInfo>\
	                                <ObjectInfo>\
		                                <BarcodeObject>\
			                                <Name>BC_BOTTOM_RIGHT</Name>\
			                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                                <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                                <LinkedObjectName></LinkedObjectName>\
			                                <Rotation>Rotation0</Rotation>\
			                                <IsMirrored>False</IsMirrored>\
			                                <IsVariable>True</IsVariable>\
			                                <Text>9999999</Text>\
			                                <Type>Code128Auto</Type>\
			                                <Size>Medium</Size>\
			                                <TextPosition>None</TextPosition>\
			                                <TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			                                <CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			                                <TextEmbedding>None</TextEmbedding>\
			                                <ECLevel>0</ECLevel>\
			                                <HorizontalAlignment>Center</HorizontalAlignment>\
			                                <QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
		                                </BarcodeObject>\
		                                <Bounds X="2592.8271484375" Y="861.055358886719" Width="2360.1728515625" Height="360" />\
	                                </ObjectInfo>\
	                                <ObjectInfo>\
		                                <TextObject>\
			                                <Name>TEXT_BOTTOM_RIGHT</Name>\
			                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                                <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                                <LinkedObjectName></LinkedObjectName>\
			                                <Rotation>Rotation0</Rotation>\
			                                <IsMirrored>False</IsMirrored>\
			                                <IsVariable>False</IsVariable>\
			                                <HorizontalAlignment>Center</HorizontalAlignment>\
			                                <VerticalAlignment>Top</VerticalAlignment>\
			                                <TextFitMode>ShrinkToFit</TextFitMode>\
			                                <UseFullFontHeight>True</UseFullFontHeight>\
			                                <Verticalized>False</Verticalized>\
			                                <StyledText />\
		                                </TextObject>\
		                                <Bounds X="2812.66015625" Y="1266.23303222656" Width="1840.33984375" Height="216" />\
	                                </ObjectInfo>\
	                                <ObjectInfo>\
		                                <BarcodeObject>\
			                                <Name>BC_TOP</Name>\
			                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                                <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                                <LinkedObjectName></LinkedObjectName>\
			                                <Rotation>Rotation0</Rotation>\
			                                <IsMirrored>False</IsMirrored>\
			                                <IsVariable>True</IsVariable>\
			                                <Text>9999999</Text>\
			                                <Type>Code128Auto</Type>\
			                                <Size>Medium</Size>\
			                                <TextPosition>None</TextPosition>\
			                                <TextFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			                                <CheckSumFont Family="Arial" Size="8" Bold="False" Italic="False" Underline="False" Strikeout="False" />\
			                                <TextEmbedding>None</TextEmbedding>\
			                                <ECLevel>0</ECLevel>\
			                                <HorizontalAlignment>Center</HorizontalAlignment>\
			                                <QuietZonesPadding Left="0" Top="0" Right="0" Bottom="0" />\
		                                </BarcodeObject>\
		                                <Bounds X="331" Y="141.055358886719" Width="4397" Height="360" />\
	                                </ObjectInfo>\
	                                <ObjectInfo>\
		                                <TextObject>\
			                                <Name>TEXT_TOP</Name>\
			                                <ForeColor Alpha="255" Red="0" Green="0" Blue="0" />\
			                                <BackColor Alpha="0" Red="255" Green="255" Blue="255" />\
			                                <LinkedObjectName></LinkedObjectName>\
			                                <Rotation>Rotation0</Rotation>\
			                                <IsMirrored>False</IsMirrored>\
			                                <IsVariable>False</IsVariable>\
			                                <HorizontalAlignment>Center</HorizontalAlignment>\
			                                <VerticalAlignment>Top</VerticalAlignment>\
			                                <TextFitMode>ShrinkToFit</TextFitMode>\
			                                <UseFullFontHeight>True</UseFullFontHeight>\
			                                <Verticalized>False</Verticalized>\
			                                <StyledText />\
		                                </TextObject>\
		                                <Bounds X="331" Y="527" Width="4412" Height="216" />\
	                                </ObjectInfo>\
                                </DieCutLabel>';
                var label = dymo.label.framework.openLabelXml(labelXml);

                //Get the value from bcdata field.
                label.setObjectText("BC_TOP", document.getElementById('patientId').value);
                label.setObjectText("BC_BOTTOM_LEFT", document.getElementById('patientId').value);
                label.setObjectText("BC_BOTTOM_RIGHT", document.getElementById('patientId').value);
                label.setObjectText("TEXT_TOP", document.getElementById('patientLName').value + ", " + document.getElementById('patientFName').value + " EMS" + document.getElementById('patientId').value);
                label.setObjectText("TEXT_BOTTOM_LEFT", "EMS" + document.getElementById('patientId').value);
                label.setObjectText("TEXT_BOTTOM_RIGHT", "EMS" + document.getElementById('patientId').value);

                // select printer to print on
                // for simplicity sake just use the first LabelWriter printer
                var printers = dymo.label.framework.getPrinters();
                if (printers.length == 0)
                    throw "No DYMO printers are installed. Install DYMO printers.";

                var printerName = "";
                for (var i = 0; i < printers.length; ++i) {
                    var printer = printers[i];
                    if (printer.printerType == "LabelWriterPrinter") {
                        printerName = printer.name;
                        break;
                    }
                }

                if (printerName == "")
                    throw "No LabelWriter printers found. Install LabelWriter printer";

                // finally print the label
                label.print(printerName);
            }
            catch (e) {
                alert(e.message || e);
            }
        }
    };

    // register onload event
    if (window.addEventListener)
        window.addEventListener("load", onload, false);
    else if (window.attachEvent)
        window.attachEvent("onload", onload);
    else
        window.onload = onload;

} ());