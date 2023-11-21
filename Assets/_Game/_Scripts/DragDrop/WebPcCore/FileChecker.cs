
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FileChecker 
{
     private List<string> _videoFormats = new List<string>
    {
        //video
        ".flv",
        ".f4v",
        ".f4p",
        ".f4a",
        ".f4b",
        ".nsv",
        ".roq",
        ".mxf",
        ".3g2",
        ".3gp",
        ".svi",
        ".m4v",
        ".mpg",
        ".mpeg",
        ".m2v",
        ".mpg",
        //".mp2",
        ".mpeg",
        ".mpe",
        ".mpv",
        ".mp4",
        ".m4p",
        ".m4v",
        ".amv",
        ".asf",
        ".viv",
        ".rmvb",
        ".rm",
        ".yuv",
        ".wmv",
        ".mov",
        ".qt",
        ".MTS",
        ".M2TS",
        ".TS",
        ".avi",
        ".mng",
        ".gifv",
        ".gif",
        ".drc",
        ".ogv",
        ".ogg",
        ".vob",
        ".flv",
        ".mkv",
        ".webm"
    };

    private List<string> _imageFormats = new List<string>
    {
        //image
        ".pdf",
        ".eps",
        ".ai",
        ".svg",
        ".svgz",
        ".jp2",
        ".j2k",
        ".jpf",
        ".jpx",
        ".jpm",
        ".mj2",
        ".ind",
        ".indd",
        ".indt",
        ".heif",
        ".heic",
        ".bmp",
        ".dib",
        ".raw",
        ".arw",
        ".cr2",
        ".nrw",
        ".k25",
        ".psd",
        ".tiff",
        ".tif",
        ".webp",
        ".gif",
        ".png",
        ".jpg",
        ".jpeg",
        ".jpe",
        ".jif",
        ".jfif",
        ".jfi",
    };

    private List<string> _otherFormats = new List<string>
    {
        //ms office
        ".xps",
        ".pub",
        ".one",
        ".ldb",
        ".mde",
        ".mdf",
        ".mdt",
        ".mdn",
        ".mda",
        ".cdb",
        ".mdb",
        ".adp",
        ".ade",
        ".laccdb",
        ".maf",
        ".mat",
        ".mar",
        ".maq",
        ".mam",
        ".accde",
        ".mdw",
        ".accda",
        ".accdt",
        ".accdr",
        ".accdb",
        ".adn",
        ".sldm",
        ".sldx",
        ".ppsm",
        ".ppsx",
        ".ppam",
        ".potm",
        ".potx",
        ".pptm",
        ".pptx",
        ".pps",
        ".pot",
        ".ppt",
        ".xlw",
        ".xll",
        ".xlam",
        ".xla",
        ".xlsb",
        ".xltm",
        ".xltx",
        ".xlsm",
        ".xlsx",
        ".xlm",
        ".xlt",
        ".xls",
        ".docb",
        ".dotm",
        ".dotx",
        ".docm",
        ".docx",
        ".wbk",
        ".dot",
        ".doc",

        //other
        ".exe",
        ".txt"
    };

    private string[] _audioFormats = new string[45]
    {
        //audio
        ".mp2",
        ".cda",
        ".8svx",
        ".webm",
        ".wv",
        ".wma",
        ".wav",
        ".vox",
        ".voc",
        ".tta",
        ".sln",
        ".rf64",
        ".raw",
        ".ra",
        ".rm",
        ".opus",
        ".ogg",
        ".oga",
        ".mogg",
        ".nmf",
        ".msv",
        ".mpc",
        ".mp3",
        ".mmf",
        ".m4p",
        ".m4b",
        ".m4a",
        ".ivs",
        ".iklax",
        ".gsm",
        ".flac",
        ".dvf",
        ".dss",
        ".dct",
        ".awb",
        ".au",
        ".ape",
        ".amr",
        ".alac",
        ".aiff",
        ".act",
        ".aax",
        ".aac",
        ".aa",
        ".3gp"
    };
    
    public ContentType CheckFile(string fileName)
    {
        string ext = new FileInfo(fileName).Extension.ToLower();

        if (_videoFormats.Any(t => ext == t.ToLower()))
            return ContentType.Video;


        if (_imageFormats.Any(t => ext == t.ToLower()))
            return ContentType.Image;


        if (_audioFormats.Any(t => ext == t.ToLower()))
            return ContentType.Audio;

        return ContentType.Nothing;
    }
}