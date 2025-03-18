using System.Text;
namespace FileHeaderCheck;
public static class FileHeaderCheck
{
    private const string ZipOrOffice2007 = "ZIP_OR_OFFICE2007";
    private const string Office2003="OFFICE2003";
    private static readonly FileTypeInfo[] FileTypes =
    [
        // Microsoft binary formats
        new ("Microsoft Office applications (Word, Powerpoint, Excel, Works)", Office2003, [0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1]),
        // Microsoft Office Open XML
        new ("Zip or Microsoft Office 2007, 2010 or 2013 document", ZipOrOffice2007, [ 0x50, 0x4B ]),
        // PDF
        new ("Adobe Portable Document file (version 1.7)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x37],"application/pdf"),
        new ("Adobe Portable Document file (version 1.6)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x36],"application/pdf"),
        new ("Adobe Portable Document file (version 1.5)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x35],"application/pdf"),
        new ("Adobe Portable Document file (version 1.4)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x34],"application/pdf"),
        new ("Adobe Portable Document file (version 1.3)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x33],"application/pdf"),
        new ("Adobe Portable Document file (version 1.2)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x32],"application/pdf"),
        new ("Adobe Portable Document file (version 1.1)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x31],"application/pdf"),
        new ("Adobe Portable Document file (version 1.0)", "pdf",   [0x25, 0x50, 0x44, 0x46, 0x2D, 0x31, 0x2E, 0x30],"application/pdf"),
        new ("Adobe Portable Document file", "pdf",    [0x25, 0x50, 0x44, 0x46]),
        // RTF
        new ("Rich Text Format", "rtf", [0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31], "application/rtf"),
        // JPEG
        new ("Samsung D807 JPEG file", "jpg",                           [0xFF, 0xD8, 0xFF, 0xDB], "image/jpeg"),
        new ("JPEG/JIFF file", "jpg",                                   [0xFF, 0xD8, 0xFF, 0xE0], "image/jpeg"),
        new ("JPEG/Exif file", "jpg",                                   [0xFF, 0xD8, 0xFF, 0xE1], "image/jpeg"),
        new ("Canon EOS-1D JPEG file", "jpg",                           [0xFF, 0xD8, 0xFF, 0xE2], "image/jpeg"),
        new ("Samsung D500 JPEG file", "jpg",                           [0xFF, 0xD8, 0xFF, 0xE3], "image/jpeg"),
        new ("Still Picture Interchange File Format (SPIFF)", "jpg",    [0xFF, 0xD8, 0xFF, 0xE8], "image/jpeg"),
        new ("JPEG/JIFF file", "jpg",                                   [0xFF, 0xD8, 0xFF, 0xEE], "image/jpeg"),
        new ("JPEG 2000 format", "jpg2",                                [0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A]),
        new ("JPEG 2000 format", "jpg2",                                [0xFF, 0x4F, 0xFF, 0x51]),
        new ("Lepton compressed JPEG image", "lep", [0xCF, 0x84, 0x01]),
        //PNG
        new ("Portable Network Graphics", "png", [0x89, 0x50, 0x4E, 0x47], "image/png"),
        
        new ("Google WebP image file", "webp", [0x52, 0x49, 0x46, 0x46], "image/webp", magicBytes2: [0x57, 0x45, 0x42, 0x50], offSet2: 8),
        new ("Windows (or device-independent) bitmap image", "bmp", [0x42, 0x4D]),
        new ("Graphics interchange format file (GIF87a)", "gif", [0x47, 0x49, 0x46, 0x38, 0x37, 0x61], "image/gif"),
        new ("Graphics interchange format file (GIF89a)", "gif", [0x47, 0x49, 0x46, 0x38, 0x39, 0x61], "image/gif"),
        new ("Image Format Bitmap file", "img", [0x00, 0x01, 0x00, 0x08, 0x00, 0x01, 0x00, 0x01, 0x01]),
        new ("ADEX Corp. ChromaGraph Graphics Card Bitmap Graphic file", "img", [0x50, 0x49, 0x43, 0x54, 0x00, 0x08]),
        new ("Img Software Set Bitmap", "img", [0x53, 0x43, 0x4D, 0x49]),
        new ("Image encoded in the JPEG XL format", "jxl", [0x00, 0x00, 0x00, 0x0C, 0x4A, 0x58, 0x4C, 0x20, 0x0D, 0x0A, 0x87, 0x0A]),
        new ("Photoshop Document file", "psd", [0x38, 0x42, 0x50, 0x53]),
        new ("Radiance High Dynamic Range image file", "hdr", [0x23, 0x3F, 0x52, 0x41, 0x44, 0x49, 0x41, 0x4E, 0x43, 0x45, 0x0A]),
        new ("Header file of a .hdr/.img pair in NIfTI format, used extensively in biomedical imaging", "hdr", [0x6E, 0x69, 0x31, 0x00]),
        new ("Canon digital camera RAW file", "crw", [0x49, 0x49, 0x1A, 0x00, 0x00, 0x00, 0x48, 0x45, 0x41, 0x50, 0x43, 0x43, 0x44, 0x52, 0x02, 0x00]),
        new ("Canon RAW Format Version 2", "cr2", [0x49, 0x49, 0x2A, 0x00, 0x10, 0x00, 0x00, 0x00, 0x43, 0x52]),
        new ("Windows icon file", "ico", [0x00, 0x00, 0x01, 0x00], "image/x-icon"),
        new ("Apple Icon Image format", "icns", [0x69, 0x63, 0x6E, 0x73]),
        new ("High Efficiency Image Container (HEIC)", "heic", [0x66, 0x74, 0x79, 0x70, 0x68, 0x65, 0x69, 0x63, 0x66, 0x74, 0x79, 0x70, 0x6D], offSet:4),
        
        new ("Kodak Cineon image", "cin", [0x50, 0x2A, 0x5F, 0xD7]),    
        new ("Digital Imaging and Communications in Medicine (DICOM) file", "dcm", [0x4D, 0x4D, 0x00, 0x2B]),
        new ("Free Lossless Image Format", "flif", [0x46, 0x4C, 0x49, 0x46]),
        new ("Adobe Photoshop Document file", "psd", [0x38, 0x42, 0x50, 0x53]),
        new ("nuru ASCII/ANSI image files", "nui", [0x4E, 0x55, 0x52, 0x55, 0x49, 0x4D, 0x47]),
        new ("nuru ASCII/ANSI palette files", "nup", [0x4E, 0x55, 0x52, 0x55, 0x50, 0x41, 0x4C]),
        new ("SMPTE DPX image (big-endian format)", "dpx", [0x53, 0x4D, 0x50, 0x44]),
        new ("SMPTE DPX image (little-endian format)", "dpx", [0x58, 0x50, 0x44, 0x53]),
        new ("OpenEXR image", "exr", [0x76, 0x2F, 0x31, 0x01]),
        new ("Better Portable Graphics format", "bpg", [0x42, 0x50, 0x47, 0xFB]),
        new ("Quite OK Image Format", "qoi", [0x71, 0x6F, 0x69, 0x66]),
        new ("IFF YUV Image", "yuvn", [0x46, 0x4F, 0x52, 0x4D], magicBytes2: [0x59, 0x55, 0x56, 0x4E], offSet2: 8),
        new ("Corel Paint Shop Pro Image file", "pspimage",[0x50, 0x61, 0x69, 0x6E, 0x74, 0x20, 0x53, 0x68, 0x6F, 0x70, 0x20, 0x50, 0x72, 0x6F, 0x20, 0x49, 0x6D, 0x61, 0x67, 0x65, 0x20, 0x46, 0x69, 0x6C, 0x65]),
        new ("Corel Paint Shop Pro browse file", "jbf", [0x4A, 0x41, 0x53, 0x43, 0x20, 0x42, 0x52, 0x4F, 0x57, 0x53, 0x20, 0x46, 0x49, 0x4C, 0x45]),
        new ("Microsoft Document Imaging file", "mdi", [0x45, 0x50]),
        new ("Photoshop image file", "psd", [0x38, 0x42, 0x50, 0x53]),
        new ("Corel Photopaint file", "cpt", [0x43, 0x50, 0x54, 0x46, 0x49, 0x4C, 0x45]),
        //TIF
        new ("BigTIFF files; Tagged Image File Format files > 4 GB", "tif", [0x4D, 0x4D, 0x00, 0x2B], "image/tiff"),
        new ("Tagged Image File Format file (big endian, i.e., LSB last in the byte; Motorola)", "tif", [0x4D, 0x4D, 0x00, 0x2A], "image/tiff"),
        new ("Tagged Image File Format file (little endian, i.e., LSB first in the byte; Intel)", "tif", [0x49, 0x49, 0x2A, 0x00], "image/tiff"),
        new ("Tagged Image File Format file", "tif", [0x49, 0x20, 0x49], "image/tiff"),
        new ("FileNet COLD document", "cold", [0xC5, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0D]),
        
        new ("shell script", "sh", [0x23, 0x21, 0x2F, 0x62, 0x69, 0x6E, 0x2F, 0x73, 0x68]),
        new ("Perl script file", "pl", [0x23, 0x21, 0x2F, 0x75, 0x73, 0x72, 0x2F, 0x62, 0x69, 0x6E, 0x2F, 0x70, 0x65, 0x72, 0x6C]),
        new ("Microsoft Developer Studio project file", "dsp", [0x23, 0x20, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x44, 0x65, 0x76, 0x65, 0x6C, 0x6F, 0x70, 0x65, 0x72, 0x20, 0x53, 0x74, 0x75, 0x64, 0x69, 0x6F]),
        new ("UTF-8 byte order mark", "txt", [0xEF,0xBB,0xBF]),
        new ("UTF-16LE byte order mark", "txt", [0xFF,0xFE]),
        new ("UTF-16BE byte order mark", "txt", [0xEF,0xFE]),
        new ("UTF-32LE byte order mark for text", "txt", [0xFF,0xFE,0x00,0x00]),
        new ("UTF-32BE byte order mark for text", "txt", [0x00,0x00,0xFE,0xFF]),
        new ("UTF-7 byte order mark for text", "txt", [0x2B, 0x2F, 0x76, 0x38]),
        new ("UTF-7 byte order mark for text", "txt", [0x2B, 0x2F, 0x76, 0x39]),
        new ("UTF-7 byte order mark for text", "txt", [0x2B, 0x2F, 0x76, 0x2B]),
        new ("UTF-7 byte order mark for text", "txt", [0x2B, 0x2F, 0x76, 0x2F]),
        new ("USCSU byte order mark for text", "txt", [0x0E, 0xFE, 0xFF]),
        new ("UTF-EBCDIC byte order mark for text", "txt", [0xDD, 0x73, 0x66, 0x73]),
        new ("Microsoft Access file", "mdb", [0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6E, 0x64, 0x61, 0x72, 0x64, 0x20, 0x4A, 0x65, 0x74, 0x20, 0x44, 0x42]),
        new ("Microsoft Access 2007 file", "accdb", [0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6E, 0x64, 0x61, 0x72, 0x64, 0x20, 0x41, 0x43, 0x45, 0x20, 0x44, 0x42]),
        new ("Outlook address file", "wab", [0x9C, 0xCB, 0xCB, 0x8D, 0x13, 0x75, 0xD2, 0x11, 0x91, 0x58, 0x00, 0xC0, 0x4F, 0x79, 0x56, 0xA4]),
        new ("Microsoft  Outlook Personal Folder File", "pst", [0x21, 0x42, 0x44]),
        //Comporess file
        //new ("ZLock Pro encrypted ZIP", "zip", [0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x01, 0x00]),
        new ("WinZip compressed archive","zip",[0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70], "application/zip"),
        new ("PKLITE compressed ZIP archive (see also PKZIP)", "zip", [0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45], "application/zip"),
        new ("PKSFX self-extracting executable compressed file (see also PKZIP)", "zip", [0x50, 0x4B, 0x53, 0x46, 0x58], "application/zip"),
        new ("7-Zip compressed file", "7z", [0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C]),
        new ("PKSFX self-extracting executable compressed file (see also PKZIP)", "zip", [0x50, 0x4B, 0x53, 0x46, 0x58]),
        new ("GZIP archive file", "gz", [0x1F, 0x8B]),
        new ("XZ compression utility using LZMA2 compression", "xz", [0xFD, 0x37, 0x7A, 0x58, 0x5A, 0x00]),
        new ("BZIP2 archive file", "bz2", [0x42, 0x5A, 0x68]),
        new ("ARC archive file", "arc", [0x1a, 0x08]),
        new ("LZ4 Frame Format   Remark: LZ4 block format does not offer any magic bytes", "lz4", [0x04, 0x22, 0x4D, 0x18]),
        new ("Roshal ARchive compressed archive v1.50 onwards", "rar", [0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00]),
        new ("Roshal ARchive compressed archive v5.00 onwards", "rar", [0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x01, 0x00]),
        new ("JARCS compressed archive", "jar", [0x4A, 0x41, 0x52, 0x43, 0x53, 0x00]),
        new ("eXtensible ARchive format", "xar", [0x78, 0x61, 0x72, 0x21]),
        new ("Zstandard compress", "zst", [0x28, 0xB5, 0x2F, 0xFD]),
        new ("xml (UTF-8 or other 8-bit encodings)", "xml", [0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20]),
        new ("xml (UTF-16LE)", "xml", [0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00]),
        new ("xml (UTF-16BE)", "xml", [0x00, 0x3C, 0x00, 0x3F, 0x00, 0x78, 0x00, 0x6D, 0x00, 0x6C, 0x00, 0x20]),
        new ("xml (UTF-32LE)", "xml", [0x3C, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0x6D, 0x00, 0x00, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00]),
        new ("xml (UTF-32BE)", "xml", [0x00, 0x00, 0x00, 0x3C, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0x6D, 0x00, 0x00, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x20]),
        new ("xml (EBCDIC)", "xml", [0x4C, 0x6F, 0xA7, 0x94, 0x93, 0x40]),
        new ("WebAssembly binary format", "wasm", [0x00, 0x61, 0x73, 0x6D]),
        new ("A commmon file extension for e-mail files", "eml", [0x52, 0x65, 0x74, 0x75, 0x72, 0x6E, 0x2D, 0x50, 0x61, 0x74, 0x68, 0x3A, 0x20]),
        new ("E-mail markup language file", "eml", [0x46, 0x72, 0x6F, 0x6D, 0x20, 0x3F, 0x3F, 0x3F]),
        new ("E-mail markup language file", "eml", [0x46, 0x72, 0x6F, 0x6D, 0x20, 0x20, 0x20]),
        new ("E-mail markup language file", "eml", [0x46, 0x72, 0x6F, 0x6D, 0x3A, 0x20]),
        new ("Generic AutoCAD drawing", "dwg", [0x41, 0x43, 0x31, 0x30]),
        //audio
        new ("RealAudio file", "ra", [0x2E, 0x52, 0x4D, 0x46, 0x00, 0x00, 0x00, 0x12, 0x00]),
        new ("RealAudio streaming media file", "ra", [0x2E, 0x72, 0x61, 0xFD, 0x00]),
        new ("MP3 file with an ID3v2 container", "mp3", [0x49, 0x44, 0x33], "audio/mpeg3"),
        new ("MPEG-1 Layer 3 file without an ID3 tag or with an ID3v1 tag (which is appended at the end of the file)","mp3",[0xFF, 0xFB], "audio/mpeg3"),
        new ("MPEG-1 Layer 3 file without an ID3 tag or with an ID3v1 tag (which is appended at the end of the file)","mp3",[0xFF, 0xF3], "audio/mpeg3"),
        new ("MPEG-1 Layer 3 file without an ID3 tag or with an ID3v1 tag (which is appended at the end of the file)","mp3",[0xFF, 0xF2], "audio/mpeg3"),
        new ("Musical Instrument Digital Interface (MIDI) sound file", "mid", [0x4D, 0x54, 0x68, 0x64]),
        new ("Waveform Audio File Format", "wav", [0x52, 0x49, 0x46, 0x46], "audio/wav", magicBytes2:[0x57, 0x41, 0x56, 0x45], offSet2: 8),
        new ("Audio Interchange File Format", "aiff", [0x46, 0x4F, 0x52, 0x4D], magicBytes2:[0x41, 0x49, 0x46, 0x46], offSet2: 8),
        new ("Free Lossless Audio Codec", "flac", [0x66, 0x4C, 0x61, 0x43]),
        new ("Au audio file format", "au", [0x2E, 0x73, 0x6E, 0x64]),
        new ("Adaptive Multi-Rate ACELP (Algebraic Code Excited Linear Prediction) Codec, commonly audio format with GSM cell phones", "amr", [0x23, 0x21, 0x41, 0x4D, 0x52]),
        new ("Audio compression format developed by Skype", "sil", [0x23, 0x21, 0x53, 0x49, 0x4C, 0x4B, 0x0A]),
        new ("Creative Voice file format", "voc", [0x43, 0x72, 0x65, 0x61, 0x74, 0x69, 0x76, 0x65, 0x20, 0x56, 0x6F, 0x69, 0x63, 0x65, 0x20, 0x46, 0x69, 0x6C, 0x65, 0x1A, 0x1A, 0x00]),
        //video
        new ("RealPlayer video file (V11 and later)", "ivr", [0x2E, 0x52, 0x45, 0x43]),
        new ("RealMedia streaming media file", "rm", [0x2E, 0x52, 0x4D, 0x46]),
        
        new ("Macromedia Shockwave Flash player file", "swf", [0x46, 0x57, 0x53]),
        new ("Shockwave Flash file (v5+)", "swf", [0x43, 0x57, 0x53]),
        
        new ("Dolby Digital Audio Coding3", "ac3", [0x0B, 0x77]),
        new ("Apple QuickTime movie file", "mov", [0x66, 0x74, 0x79, 0x70, 0x71, 0x74], "video/quicktime", offSet: 4),
        new ("Audio-only MPEG-4 files", "m4a", [0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20], offSet: 4),
        new ("Matroska open movie format", "mkv", [0x1A, 0x45, 0xD5, 0xA3, 0x93, 0x42, 0x82, 0x88, 0x6D, 0x61, 0x74, 0x72, 0x6F, 0x73, 0x6B]),
        
        new ("Audio Video Interleave video format", "avi", [0x52, 0x49, 0x46, 0x46], "video/avi", magicBytes2:[0x41, 0x56, 0x49, 0x20], offSet2:8),
        new("crosoft Windows Media Audio/Video File (Advanced Streaming Format)", "wmv", [0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11, 0xA6, 0xD9, 0x00, 0xAA, 0x00, 0x62, 0xCE, 0x6C], "video/x-ms-wmv"),
        new ("ISO Base Media file (MPEG-4)", "mp4", [0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D], "video/mp4", 4),
        new ("MP4 v2 [ISO 14496-14] MPEG-4 video file", "mp4", [0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32], "video/mp4", 4),
        new ("MP4 v1 [ISO 14496-1:ch13]  MPEG-4 video file", "mp4", [0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x31], "video/mp4", 4),
        new ("MPEG-4 video file", "mp4", [0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56], "video/mp4", 4),
        new ("open source media container format, video", "ogg", [0x4F, 0x67, 0x67, 0x53]),
        new ("Flash video file", "flv", [0x46, 0x4C, 0x56, 0x01]),
        new ("Multimedia playlist", "m3u8", [0x23, 0x45, 0x58, 0x54, 0x4D, 0x33, 0x55]),
        new ("MPEG Program Stream (MPEG-1 Part 1 (essentially identical) and MPEG-2 Part 1)", "vob", [0x00, 0x00, 0x01, 0xBA]),
        new ("3rd Generation Partnership Project 3GPP and 3GPP2 multimedia files", "3gp", [0x66, 0x74, 0x79, 0x70, 0x33, 0x67], offSet:4),
        new ("Matroska media container, including WebM", "webm", [0x1A, 0x45, 0xDF, 0xA3]),
        //other
        new ("vCard file", "vcf", [0x42, 0x45, 0x47, 0x49, 0x4E, 0x3A, 0x56, 0x43, 0x41, 0x52, 0x44]),
        
        new ("Apple Disk Image file", "dmg", [0x6B, 0x6F, 0x6C, 0x79]),
        new ("Binary Property List file", "plist", [0x62, 0x70, 0x6C, 0x69, 0x73, 0x74]),
        new ("macOS file Alias (Symbolic link)", "alias", [0x62, 0x6F, 0x6F, 0x6B, 0x00, 0x00, 0x00, 0x00, 0x6D, 0x61, 0x72, 0x6B, 0x00, 0x00, 0x00, 0x00]),
        new ("DOS ZM executable and its descendants (rare)", "exe", [0x5A, 0x4D]),
        new ("Windows shortcut file", "lnk", [0x4C, 0x00, 0x00, 0x00, 0x01, 0x14, 0x02, 0x00]),
        new ("Windows 9x printer spool file", "shd", [0x4B, 0x49, 0x00, 0x00]),
        new ("Microsoft SQL Server 2000 database", "mdf", [0x01, 0x0F, 0x00, 0x00]),
        new ("SQLite Database", "sqlitedb", [0x53, 0x51, 0x4C, 0x69, 0x74, 0x65, 0x20, 0x66, 0x6F, 0x72, 0x6D, 0x61, 0x74, 0x20, 0x33, 0x00]),
        new ("linux deb file", "deb", [0x21, 0x3C, 0x61, 0x72, 0x63, 0x68, 0x3E, 0x0A]),
        new ("RedHat Package Manager (RPM) package", "rpm", [0xED, 0xAB, 0xEE, 0xDB]),
        new ("internal WAD (main resource file of Doom)", "iwad", [0x49, 0x57, 0x41, 0x44]),
        new ("Telegram Desktop File", "TDF$", [0x54, 0x44, 0x46, 0x24]),
        new ("Telegram Desktop Encrypted File", "TDEF", [0x54, 0x44, 0x45, 0x46]),
        new ("Data stored in version 4 of the Hierarchical Data Format", "hdf4", [0x0E, 0x03, 0x13, 0x01]),
        new ("Data stored in version 5 of the Hierarchical Data Format", "hdf5", [0x89, 0x48, 0x44, 0x46, 0x0D, 0x0A, 0x1A, 0x0A]),
        new ("Java class file, Mach-O Fat Binary", "class", [0xCA, 0xFE, 0xBA, 0xBE]),
        new ("MS Windows HtmlHelp Data", "chm", [0x49, 0x54, 0x53, 0x46, 0x03, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00]),
        new ("ISO9660 CD/DVD image file", "iso", [0x43, 0x44, 0x30, 0x30, 0x31]),
        new ("Nintendo Entertainment System image file", "nes", [0x4E, 0x45, 0x53]),            
        new ("Dalvik Executable", "dex", [0x64, 0x65, 0x78, 0x0A, 0x30, 0x33, 0x35, 0x00]),
        new ("KDB file", "kdb", [0x25, 0x30, 0x03, 0x02, 0x00, 0x00, 0x00, 0x00, 0x58, 0x35, 0x30, 0x39, 0x4B, 0x45, 0x59]),
        new ("Google Chrome extension[47] or packaged app", "crx", [0x43, 0x72, 0x32, 0x34]),
        new ("Microsoft Cabinet file", "cab", [0x4D, 0x53, 0x43, 0x46]),
        new ("InstallShield CAB Archive File", "cab", [0x49, 0x53, 0x63, 0x28]),
        new ("Lua bytecode", "luac", [0x1B, 0x4C, 0x75, 0x61]),
        new ("qcow file format", "qcow", [0x51, 0x46, 0x49]),
        new ("Blender File Format", "blend", [0x42, 0x4C, 0x45, 0x4E, 0x44, 0x45, 0x52]),
    ];
    public static FileTypeInfo? GetFileType(Stream stream)
    {
        if(stream.CanSeek && stream.Position != 0)
            stream.Position = 0;
        byte[] buffer = new byte[36];
        stream.ReadExactly(buffer);
        var result = GetFileTypeResult(buffer.AsSpan());
        return result?.Extension switch
        {
            ZipOrOffice2007 => GetFileTypeByZipOrOffice2007(stream),
            Office2003 => GetFileTypeByOffice2003(stream),
            _ => result,
        };;
    }
    private static FileTypeInfo? GetFileTypeResult(Span<byte> buffer)
    {
        FileTypeInfo? result = null;
        var maxMatchCount = 0;
        foreach (var t in FileTypes)
        {                
            var matchCount = t.MagicBytes.AsSpan().CommonPrefixLength(buffer[t.Offset..]);
            if (matchCount < t.MagicBytes!.Length) continue;
            if (t.MagicBytes2 != null)
            {
                matchCount += t.MagicBytes2.AsSpan().CommonPrefixLength(buffer[t.OffSet2..]);
            }
            if(matchCount > maxMatchCount)
            {
                maxMatchCount = matchCount;
                result = t;
            }
        }
        return result;
    }
    private static FileTypeInfo? GetFileTypeByOffice2003(Stream stream)
     {
        var buffer = new byte[3000];
        stream.ReadExactly(buffer);
        var content = Encoding.ASCII.GetString(buffer);
        var result = content switch
        {
            var str when str.Contains("MSWord") => 
                str.Contains("Word_sjabloon") || str.Contains("Word_template") ?
                    new FileTypeInfo("Microsoft Word template", "dot", null) :
                    new FileTypeInfo("Microsoft Word binary format", "doc", null, "application/ms-doc"),
            var str when str.Contains("MS PowerPoint") =>
                new FileTypeInfo("Microsoft PowerPoint binary format", "ppt", null, "application/vnd.ms-powerpoint"),
            var str when str.Contains("Microsoft Visio") =>
                new FileTypeInfo("Microsoft Visio binary format", "vsd", null),
            _ => null,
        };
        if(result == null)
        {
            //excel文件中的"Workbook"在文件末尾，所以需要循环读取
            Span<byte> buffer1 = new byte[2048];
            int count;
            do
            {
                count = stream.ReadAtLeast(buffer1, buffer1.Length, false);
                if (Encoding.Unicode.GetString(buffer1).Contains("Workbook"))
                {
                    //office 97-2003
                    return new FileTypeInfo("Microsoft Excel binary format", "xls", null, "application/vnd.ms-excel");
                }
            }
            while (count > 0);
        }
        return result;
    }
    private static FileTypeInfo GetFileTypeByZipOrOffice2007(Stream stream)
    {
        var buffer = new byte[5000];
        stream.ReadExactly(buffer);
        var content = Encoding.ASCII.GetString(buffer);
        return content switch
        {
            var str when str.Contains("word/_rels/") =>
                new FileTypeInfo("Microsoft Word open XML document format", "docx", null, "application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
            var str when str.Contains("xl/_rels/workbook") =>
                new FileTypeInfo("Microsoft Excel open XML document format", "xlsx", null, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
            var str when str.Contains("ppt/slides/_rels") =>
                new FileTypeInfo("Microsoft PowerPoint open XML document format", "pptx", null, "application/vnd.openxmlformats-officedocument.presentationml.presentation"),
            var str when str.Contains("Microsoft Visio") =>
                new FileTypeInfo("Microsoft Visio binary format", "vsd", null),
            var str when str.Contains("Microsoft Publisher") =>
                new FileTypeInfo("Microsoft Publisher Document", "pub", null),
            var str when str.Contains("CHNKWKS") =>
                new FileTypeInfo("Microsoft Works", "wks", null),
            //还需判断 aar apk epub ipa jar kmz maff msix odp ods odt pk3 pk4 usdz vsdx xpi whl
            _ => new FileTypeInfo("Zip compressed archive", "zip", null),
        };
    }
}
