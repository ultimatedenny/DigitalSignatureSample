using Syncfusion.Pdf;
using Syncfusion.Pdf.Security;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;

namespace DigitalSignatureSample {
    internal class Program {
        static void Main(string[] args) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBPh8sVXJ0S0J+XE9Hd1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xSdEVkWHhfcnVTRGNbWQ==");

            PdfDocument document = new();
            PdfPageBase page=document.Pages.Add();
            _ = page.Graphics;
            FileStream certificateStream = new("../../../Data/DENIANDREAN.pfx", FileMode.Open, FileAccess.Read);
            PdfCertificate pdfCert = new(certificateStream, "115659deni");
            PdfSignature signature = new(document, page, pdfCert, "Signature")
            {
                Bounds = new RectangleF(new PointF(0, 0), new SizeF(100, 100))
            };
            FileStream imageStream = new("../../../Data/signature.png", FileMode.Open, FileAccess.Read);
            PdfBitmap signatureImage = new(imageStream);
            signature.ContactInfo= "johndoe@owned.us";
            signature.LocationInfo= "Honolulu, Hawaii";
            signature.Reason= "I am author of this document.";
            signature.Appearance.Normal.Graphics.DrawImage(signatureImage, new RectangleF(0, 0, 100, 100));

            MemoryStream stream = new();
            document.Save(stream);
            document.Close(true);
            stream.Position = 0;
            File.WriteAllBytes("SignedDocument.pdf", stream.ToArray());
        }
    }
}