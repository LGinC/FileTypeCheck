# FileTypeCheck
Check File Type By MagicBytes

# Usage
```CSharp
using var fs = await new HttpClient().GetStreamAsync("https://file-examples.com/storage/feba87552467d8774957b57/2017/10/file_example_favicon.ico");
var result = FileTypeCheck.GetFileType(fs);
Console.WriteLine($"{result?.Name}  .{result?.Extension}" );
```


# Support File Type

| 描述 | 扩展名 | 魔数 (十六进制) | MIME类型 |
|------|--------|----------------|----------|
| Microsoft Office 应用程序 | Office2003 | D0 CF 11 E0 A1 B1 1A E1 | - |
| Zip 或 Microsoft Office 2007+ | ZIP_OR_OFFICE2007 | 50 4B | - |
| Adobe PDF (v1.7) | pdf | 25 50 44 46 2D 31 2E 37 | application/pdf |
| Adobe PDF (v1.6) | pdf | 25 50 44 46 2D 31 2E 36 | application/pdf |
| Adobe PDF (v1.5) | pdf | 25 50 44 46 2D 31 2E 35 | application/pdf |
| Adobe PDF (v1.4) | pdf | 25 50 44 46 2D 31 2E 34 | application/pdf |
| Adobe PDF (v1.3) | pdf | 25 50 44 46 2D 31 2E 33 | application/pdf |
| Adobe PDF (v1.2) | pdf | 25 50 44 46 2D 31 2E 32 | application/pdf |
| Adobe PDF (v1.1) | pdf | 25 50 44 46 2D 31 2E 31 | application/pdf |
| Adobe PDF (v1.0) | pdf | 25 50 44 46 2D 31 2E 30 | application/pdf |
| Adobe PDF (通用) | pdf | 25 50 44 46 | - |
| Rich Text Format | rtf | 7B 5C 72 74 66 31 | application/rtf |
| JPEG (Samsung D807) | jpg | FF D8 FF DB | image/jpeg |
| JPEG/JIFF | jpg | FF D8 FF E0 | image/jpeg |
| JPEG/Exif | jpg | FF D8 FF E1 | image/jpeg |
| JPEG (Canon EOS-1D) | jpg | FF D8 FF E2 | image/jpeg |
| JPEG (Samsung D500) | jpg | FF D8 FF E3 | image/jpeg |
| SPIFF | jpg | FF D8 FF E8 | image/jpeg |
| JPEG/JIFF | jpg | FF D8 FF EE | image/jpeg |
| JPEG 2000 | jpg2 | 00 00 00 0C 6A 50 20 20 0D 0A 87 0A | - |
| JPEG 2000 (另一种格式) | jpg2 | FF 4F FF 51 | - |
| Portable Network Graphics | png | 89 50 4E 47 | image/png |
| Google WebP | webp | 52 49 46 46 + 57 45 42 50 | image/webp |
| Windows 位图 | bmp | 42 4D | - |
| GIF (87a) | gif | 47 49 46 38 37 61 | image/gif |
| GIF (89a) | gif | 47 49 46 38 39 61 | image/gif |
| Windows 图标 | ico | 00 00 01 00 | image/x-icon |
| TIFF (大端) | tif | 4D 4D 00 2A | image/tiff |
| TIFF (小端) | tif | 49 49 2A 00 | image/tiff |
| MP3 (ID3v2) | mp3 | 49 44 33 | audio/mpeg3 |
| MP3 (无ID3或ID3v1) | mp3 | FF FB | audio/mpeg3 |
| MP3 (无ID3或ID3v1) | mp3 | FF F3 | audio/mpeg3 |
| MP3 (无ID3或ID3v1) | mp3 | FF F2 | audio/mpeg3 |
| MIDI | mid | 4D 54 68 64 | - |
| WAV | wav | 52 49 46 46 + 57 41 56 45 | audio/wav |
| FLAC | flac | 66 4C 61 43 | - |
| QuickTime | mov | 66 74 79 70 71 74 | video/quicktime |
| Matroska | mkv | 1A 45 D5 A3 93 42 82 88 6D 61 74 72 6F 73 6B | - |
| AVI | avi | 52 49 46 46 + 41 56 49 20 | video/avi |
| Windows Media | wmv | 30 26 B2 75 8E 66 CF 11 A6 D9 00 AA 00 62 CE 6C | video/x-ms-wmv |
| MP4 | mp4 | 66 74 79 70 69 73 6F 6D | video/mp4 |
| MP4 v2 | mp4 | 66 74 79 70 6D 70 34 32 | video/mp4 |
| MP4 v1 | mp4 | 66 74 79 70 6D 70 34 31 | video/mp4 |
| Flash Video | flv | 46 4C 56 01 | - |
| WinZip | zip | 57 69 6E 5A 69 70 | application/zip |
| PKLITE ZIP | zip | 50 4B 4C 49 54 45 | application/zip |
| PKSFX ZIP | zip | 50 4B 53 46 58 | application/zip |
| 7-Zip | 7z | 37 7A BC AF 27 1C | - |
| GZIP | gz | 1F 8B | - |
| BZIP2 | bz2 | 42 5A 68 | - |
| RAR (v1.50+) | rar | 52 61 72 21 1A 07 00 | - |
| RAR (v5.00+) | rar | 52 61 72 21 1A 07 01 00 | - |
| XML (UTF-8) | xml | 3C 3F 78 6D 6C 20 | - |
| XML (UTF-16LE) | xml | 3C 00 3F 00 78 00 6D 00 | - |
| WebAssembly | wasm | 00 61 73 6D | - |
| Email | eml | 52 65 74 75 72 6E 2D 50 61 74 68 3A 20 | - |
| SQLite 数据库 | sqlitedb | 53 51 4C 69 74 65 20 66 6F 72 6D 61 74 20 33 00 | - |
| Java 类文件 | class | CA FE BA BE | - |