using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class PdfService
{
    private readonly IConverter _converter;

    public PdfService(IConverter converter)
    {
        _converter = converter;
        LoadNativeLibrary();
    }

    private void LoadNativeLibrary()
    {
        var architecture = IntPtr.Size == 8 ? "64bit" : "32bit";
        var libraryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libs", architecture, "libwkhtmltox.dll");

        if (!File.Exists(libraryPath))
        {
            throw new DllNotFoundException($"The required DLL was not found: {libraryPath}");
        }

        var result = NativeLibrary.Load(libraryPath);
        if (result == IntPtr.Zero)
        {
            throw new BadImageFormatException("Failed to load the native library.");
        }
    }

    public byte[] GeneratePdf(string htmlContent)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
            },
            Objects = {
                new ObjectSettings()
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    LoadSettings = { BlockLocalFileAccess = false },
                }
            }
        };

        try
        {
            return _converter.Convert(doc);
        }
        catch (DllNotFoundException dllEx)
        {
            Console.Error.WriteLine($"DLL not found: {dllEx.Message}");
            throw new ApplicationException("Required DLL not found for PDF generation.", dllEx);
        }
        catch (BadImageFormatException badImageEx)
        {
            Console.Error.WriteLine($"Bad image format: {badImageEx.Message}");
            throw new ApplicationException("Incorrect format for the PDF generation library.", badImageEx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while generating the PDF: {ex.Message}");
            throw new ApplicationException("An error occurred while generating the PDF.", ex);
        }
    }
}
