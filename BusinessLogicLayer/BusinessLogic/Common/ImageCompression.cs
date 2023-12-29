using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class ImageCompression
{
    public static byte[] CompressImage(byte[] originalImageBytes, int targetSizeKB)
    {
        using (MemoryStream originalStream = new MemoryStream(originalImageBytes))
        {
            using (Image originalImage = Image.FromStream(originalStream))
            {
                int quality = 90; // Initial quality setting

                do
                {
                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        SaveJpeg(originalImage, compressedStream, quality);

                        if (compressedStream.Length / 1024 <= targetSizeKB)
                        {
                            // If the compressed image is within the target size, return its bytes
                            return compressedStream.ToArray();
                        }

                        // If the compressed image is still larger than the target size, reduce quality
                        quality -= 10;
                    }
                } while (quality > 0);

                // If the loop completes and no suitable compression is found, return original bytes
                return originalImageBytes;
            }
        }
    }

    private static void SaveJpeg(Image image, Stream stream, int quality)
    {
        EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
        ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Jpeg);

        if (jpegCodec == null)
        {
            throw new Exception("JPEG codec not found");
        }

        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = qualityParam;

        image.Save(stream, jpegCodec, encoderParams);
    }

    private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }

        return null;
    }
}
