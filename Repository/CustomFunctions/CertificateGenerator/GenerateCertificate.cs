using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CustomFunctions.CertificateGenerator
{
    public class GenerateCertificate
    {
        static void CertificateGenerator()
        {
            int width = 600;
            int height = 400;

            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                //This first part is for the background graphics

                // Define colors
                Color color1 = ColorTranslator.FromHtml("#293362");
                Color color2 = ColorTranslator.FromHtml("#eec02b");
                Color color3 = ColorTranslator.FromHtml("#f8f8f8");

                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(color3);

                //aqua infront (short sq line is 125, long sq line is 300)
                PointF[] triangle1Points = {
                new PointF(0, 280),
                new PointF(300, 400),
                new PointF(0, 400)
            };

                //gold under (short sq line is 125, long sq line is 185, away from edge 40)
                PointF[] triangle2Points = {
                new PointF(0, 235),
                new PointF(0, 360),
                new PointF(185, 360)
            };

                //aqua infront
                PointF[] triangle3Points = {
                new PointF(600, 0),
                new PointF(600, 125),
                new PointF(300, 0)
            };

                //gold under
                PointF[] triangle4Points = {
                new PointF(600, 40),
                new PointF(600, 165),
                new PointF(415, 40)
            };

                //outer bg rect white fill no border outline
                Rectangle rect1 = new Rectangle(40, 40, width - 80, height - 80);

                //inner bg rect no fill gold border outline
                Rectangle rect2 = new Rectangle(55, 55, width - 110, height - 110);

                using (SolidBrush brush2 = new SolidBrush(color2))
                {
                    graphics.FillPolygon(brush2, triangle2Points);
                    graphics.FillPolygon(brush2, triangle4Points);
                }

                using (SolidBrush brush1 = new SolidBrush(color1))
                {
                    graphics.FillPolygon(brush1, triangle1Points);
                    graphics.FillPolygon(brush1, triangle3Points);
                }

                using (Brush whiteBrush = new SolidBrush(Color.White))
                {
                    graphics.FillRectangle(whiteBrush, rect1);
                }

                using (Pen goldPen = new Pen(Color.Gold, 2))
                {
                    graphics.DrawRectangle(goldPen, rect2);
                }

                //This second part is for the text details
                Image image1 = Image.FromFile("..\\..\\..\\Icon\\-45.png");
                Image image2 = Image.FromFile("..\\..\\..\\Icon\\dog.png");
                string userName = "Mr.Vinny";
                string subject = "How to Win Rate 101";
                string completionDate = DateTime.Now.ToString("MMMM dd, yyyy");
                string issuer = "Dante Limbus";

                // Set font
                Font titleh1Font = new Font("Times New Roman", 36, FontStyle.Bold);
                Font titleh2Font = new Font("Times New Roman", 32, FontStyle.Bold);
                Font titleh3Font = new Font("Times New Roman", 28, FontStyle.Bold);
                Font titleh4Font = new Font("Times New Roman", 24, FontStyle.Bold);
                Font titleh5Font = new Font("Times New Roman", 20, FontStyle.Bold);
                Font textFontNorm = new Font("Times New Roman", 16);
                Font textFontBold = new Font("Times New Roman", 16, FontStyle.Bold);
                Font textFontItal = new Font("Times New Roman", 16, FontStyle.Italic);
                Font textFontSmall = new Font("Times New Roman", 12);
                Font nameFont = new Font("Edwardian Script ITC", 24);
                Font sigFontBold = new Font("Arial", 8, FontStyle.Bold);

                // Set string format
                /*
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                */

                // Draw title
                string title1 = "Certificate";
                string title2 = "of Achievement";
                SizeF titleSize1 = graphics.MeasureString(title1, titleh2Font);
                SizeF titleSize2 = graphics.MeasureString(title2, textFontBold);
                graphics.DrawString(title1, titleh2Font, Brushes.Black, (width - titleSize1.Width) / 2, 60);
                graphics.DrawString(title2, textFontBold, Brushes.Black, (width - titleSize2.Width) / 2, titleSize1.Height + 30 + titleSize2.Height);

                // Draw user name
                SolidBrush brushUName = new SolidBrush(color1);
                SolidBrush brushUText = new SolidBrush(ColorTranslator.FromHtml("#ac9552"));
                string userText = "This certificate is present to";
                SizeF userTextSize = graphics.MeasureString(userText, textFontSmall);
                SizeF userNameSize = graphics.MeasureString(userName, nameFont);
                graphics.DrawString(userText, textFontSmall, brushUText, (width - userTextSize.Width) / 2, 155);
                graphics.DrawString(userName, nameFont, brushUName, (width - userNameSize.Width) / 2, 175);

                // Draw underline under the username
                float underlineStartX = (width - userNameSize.Width) / 2 - 25;
                float underlineEndX = underlineStartX + userNameSize.Width + 50;
                float underlineY = 175 + userNameSize.Height / 2 + 8; // Adjust the Y position as needed
                using (Pen underlinePen = new Pen(Color.Black, 1))
                {
                    graphics.DrawLine(underlinePen, underlineStartX, underlineY, underlineEndX, underlineY);
                }

                // Draw subject
                string subjectText = "for their excellent performance in";
                SizeF subjectTextSize = graphics.MeasureString(subjectText, textFontSmall);
                SizeF subjectSize = graphics.MeasureString(subject, titleh5Font);
                graphics.DrawString(subjectText, textFontSmall, brushUText, (width - subjectTextSize.Width) / 2, 220);
                graphics.DrawString(subject, titleh5Font, brushUName, (width - subjectSize.Width) / 2, 240);

                // Insert the image into the bitmap
                // Adjust the position and size as needed
                Image resizedImage1 = ResizeImage(image1, 45, 45);
                Image resizedImage2 = ResizeImage(image2, 60, 45);
                graphics.DrawImage(resizedImage1, new Rectangle(90, 70, resizedImage1.Width, resizedImage1.Height));
                graphics.DrawImage(resizedImage2, new Rectangle(width - 150, 70, resizedImage2.Width, resizedImage2.Height));

                // Draw issuer and the signing line
                float signUnderlineStartX = width - 210;
                float signUnderlineEndX = signUnderlineStartX + 125;
                float signUnderlineY = height - 95;
                using (Pen underlinePen = new Pen(Color.Black, 1))
                {
                    graphics.DrawLine(underlinePen, signUnderlineStartX, signUnderlineY, signUnderlineEndX, signUnderlineY);
                }
                SizeF sigTextSize = graphics.MeasureString(issuer, sigFontBold);
                float issuerTextX = signUnderlineStartX + (signUnderlineEndX - signUnderlineStartX - sigTextSize.Width) / 2 + 5;
                graphics.DrawString(issuer, sigFontBold, brushUName, issuerTextX, signUnderlineY + 10);

                //graphics.DrawString(issuer, sigFontBold, brushUName, width - sigTextSize.Width / 2, 320);


                // Save the bitmap
                // TODO: upload this generated bitmap to supabase
                bitmap.Save("..\\..\\..\\course_certificate.png");
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        /// 
        ///source:https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
