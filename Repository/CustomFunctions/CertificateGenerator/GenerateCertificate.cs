using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Repository.CustomFunctions.CertificateGenerator
{
    public static class GenerateCertificate
    {
        public async static Task<IFormFile> CertificateGenerator(string username, string subject)
        {
            int originalWidth = 600;
            int originalHeight = 400;
            int newWidth = 1000;
            int newHeight = 667;

            float scaleX = (float)newWidth / originalWidth;
            float scaleY = (float)newHeight / originalHeight;

            using (Bitmap bitmap = new Bitmap(newWidth, newHeight))
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
                    new PointF(0 * scaleX, 280 * scaleY),
                    new PointF(300 * scaleX, 400 * scaleY),
                    new PointF(0 * scaleX, 400 * scaleY)
                };

                //gold under (short sq line is 125, long sq line is 185, away from edge 40)
                PointF[] triangle2Points = {
                    new PointF(0 * scaleX, 235 * scaleY),
                    new PointF(0 * scaleX, 360 * scaleY),
                    new PointF(185 * scaleX, 360 * scaleY)
                };

                //aqua infront
                PointF[] triangle3Points = {
                    new PointF(600 * scaleX, 0 * scaleY),
                    new PointF(600 * scaleX, 125 * scaleY),
                    new PointF(300 * scaleX, 0 * scaleY)
                };

                //gold under
                PointF[] triangle4Points = {
                    new PointF(600 * scaleX, 40 * scaleY),
                    new PointF(600 * scaleX, 165 * scaleY),
                    new PointF(415 * scaleX, 40 * scaleY)
                };

                //outer bg rect white fill no border outline
                Rectangle rect1 = new Rectangle((int)(40 * scaleX), (int)(40 * scaleY), (int)(newWidth - 80 * scaleX), (int)(newHeight - 80 * scaleY));

                //inner bg rect no fill gold border outline
                Rectangle rect2 = new Rectangle((int)(55 * scaleX), (int)(55 * scaleY), (int)(newWidth - 110 * scaleX), (int)(newHeight - 110 * scaleY));

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

                using (Pen goldPen = new Pen(Color.Gold, 3))
                {
                    graphics.DrawRectangle(goldPen, rect1);
                }

                //This second part is for the text details
                Image image1 = Image.FromFile("Icon\\-45.png");
                Image image2 = Image.FromFile("Icon\\dog.png");

                string completionDate = DateTime.Now.ToString("MMMM dd, yyyy");
                string issuer = "Dante Limbus";

                // Set font
                Font titleh1Font = new Font("Times New Roman", 36 * scaleY, FontStyle.Bold);
                Font titleh2Font = new Font("Times New Roman", 32 * scaleY, FontStyle.Bold);
                Font titleh3Font = new Font("Times New Roman", 28 * scaleY, FontStyle.Bold);
                Font titleh4Font = new Font("Times New Roman", 24 * scaleY, FontStyle.Bold);
                Font titleh5Font = new Font("Times New Roman", 20 * scaleY, FontStyle.Bold);
                Font textFontNorm = new Font("Times New Roman", 16 * scaleY);
                Font textFontBold = new Font("Times New Roman", 16 * scaleY, FontStyle.Bold);
                Font textFontItal = new Font("Times New Roman", 16 * scaleY, FontStyle.Italic);
                Font textFontSmall = new Font("Times New Roman", 12 * scaleY);
                Font nameFont = new Font("Edwardian Script ITC", 24 * scaleY);
                Font sigFontBold = new Font("Arial", 8 * scaleY, FontStyle.Bold);

                // Draw title
                string title1 = "Certificate";
                string title2 = "of Achievement";
                SizeF titleSize1 = graphics.MeasureString(title1, titleh2Font);
                SizeF titleSize2 = graphics.MeasureString(title2, textFontBold);
                graphics.DrawString(title1, titleh2Font, Brushes.Black, (newWidth - titleSize1.Width) / 2, 60 * scaleY);
                graphics.DrawString(title2, textFontBold, Brushes.Black, (newWidth - titleSize2.Width) / 2, titleSize1.Height + 30 * scaleY + titleSize2.Height);

                // Draw user name
                SolidBrush brushUName = new SolidBrush(color1);
                SolidBrush brushUText = new SolidBrush(ColorTranslator.FromHtml("#ac9552"));
                string userText = "This certificate is present to";
                SizeF userTextSize = graphics.MeasureString(userText, textFontSmall);
                SizeF userNameSize = graphics.MeasureString(username, nameFont);
                graphics.DrawString(userText, textFontSmall, brushUText, (newWidth - userTextSize.Width) / 2, 155 * scaleY);
                graphics.DrawString(username, nameFont, brushUName, (newWidth - userNameSize.Width) / 2, 175 * scaleY);

                // Draw underline under the username
                float underlineStartX = (newWidth - userNameSize.Width) / 2 - 25 * scaleX;
                float underlineEndX = underlineStartX + userNameSize.Width + 50 * scaleX;
                float underlineY = 175 * scaleY + userNameSize.Height / 2 + 8 * scaleY; // Adjust the Y position as needed
                using (Pen underlinePen = new Pen(Color.Black, 1))
                {
                    graphics.DrawLine(underlinePen, underlineStartX, underlineY, underlineEndX, underlineY);
                }

                // Draw subject
                string subjectText = "for their excellent performance in";
                SizeF subjectTextSize = graphics.MeasureString(subjectText, textFontSmall);
                SizeF subjectSize = graphics.MeasureString(subject, titleh5Font);
                graphics.DrawString(subjectText, textFontSmall, brushUText, (newWidth - subjectTextSize.Width) / 2, 220 * scaleY);
                graphics.DrawString(subject, titleh5Font, brushUName, (newWidth - subjectSize.Width) / 2, 240 * scaleY);

                // Insert the image into the bitmap
                // Adjust the position and size as needed
                Image resizedImage1 = ResizeImage(image1, (int)(45 * scaleX), (int)(45 * scaleY));
                Image resizedImage2 = ResizeImage(image2, (int)(60 * scaleX), (int)(45 * scaleY));
                graphics.DrawImage(resizedImage1, new Rectangle((int)(90 * scaleX), (int)(70 * scaleY), resizedImage1.Width, resizedImage1.Height));
                graphics.DrawImage(resizedImage2, new Rectangle((int)(newWidth - 150 * scaleX), (int)(70 * scaleY), resizedImage2.Width, resizedImage2.Height));

                // Draw issuer and the signing line
                float signUnderlineStartX = newWidth - 210 * scaleX;
                float signUnderlineEndX = signUnderlineStartX + 125 * scaleX;
                float signUnderlineY = newHeight - 95 * scaleY;
                using (Pen underlinePen = new Pen(Color.Black, 1))
                {
                    graphics.DrawLine(underlinePen, signUnderlineStartX, signUnderlineY, signUnderlineEndX, signUnderlineY);
                }
                SizeF sigTextSize = graphics.MeasureString(issuer, sigFontBold);
                float issuerTextX = signUnderlineStartX + (signUnderlineEndX - signUnderlineStartX - sigTextSize.Width) / 2 + 5 * scaleX;
                graphics.DrawString(issuer, sigFontBold, brushUName, issuerTextX, signUnderlineY + 10 * scaleY);

                string fileName = $"cert_{username}_{subject}.png";

                // Save the bitmap
                var stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;

                IFormFile file = new FormFile(stream, 0, stream.Length, "certificate", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
                return file;
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

        /*public static async Task<IFormFile> ToIFormFile(this Bitmap bitmap, string fileName)
        {
                
        }*/
    }
}
