using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    public override Stage CreateStage()
    {
        Stage stage1 = new()
        {
            Next = null,
            Folders = new()
            {
                new()
                {
                    Name = "home",
                    Files = new()
                    {
                        new()
                        {
                            FileType = File.Type.TORRENT,
                            Count = 5
                        },
                        new()
                        {
                            FileType = File.Type.GITIGNORE,
                            Count = 5
                        },
                        new()
                        {
                            FileType = File.Type.SH,
                            Count = 5
                        },
                        new()
                        {
                            FileType = File.Type.C,
                            Count = 5
                        },
                        new()
                        {
                            FileType = File.Type.H,
                            Count = 5
                        }
                    }
                }
            },
        };

        List<File> files2 = new()
        {
            new()
            {
                FileType = File.Type.TXT,
                Count = 5
            },
            new()
            {
                FileType = File.Type.PDF,
                Count = 2
            },
            new()
            {
                FileType = File.Type.PPTX,
                Count = 3
            },
            new()
            {
                FileType = File.Type.DOCX,
                Count = 2
            },
            new()
            {
                FileType = File.Type.XLSX,
                Count = 2
            },
            new()
            {
                FileType = File.Type.ZIP,
                Count = 5
            },
            new()
            {
                FileType = File.Type.TORRENT,
                Count = 2
            }
        };

        Stage stage2 = new()
        {
            Next = stage1,
            Folders = new()
            {
                new()
                {
                    Name = "alex",
                    Files = files2
                },
                new()
                {
                    Name = "jonathan",
                    Files = files2
                },
                new()
                {
                    Name = "edouard",
                    Files = files2
                },
                new()
                {
                    Name = "farien",
                    Files = files2
                },
                new()
                {
                    Name = "julienne",
                    Files = files2
                },
                new()
                {
                    Name = "lulu",
                    Files = files2
                },
                new()
                {
                    Name = "brenain",
                    Files = files2
                },
                new()
                {
                    Name = "gabstophe",
                    Files = files2
                },
            },
        };

        List<File> file3images = new()
        {
            new()
            {
                FileType = File.Type.PNG,
                Count = 10
            },
            new()
            {
                FileType = File.Type.JPEG,
                Count = 10
            },
        };

        List<File> file3musics = new()
        {
            new()
            {
                FileType = File.Type.MP3,
                Count = 20
            },
        };

        List<File> file3documents = new()
        {
            new()
            {
                FileType = File.Type.PDF,
                Count = 5
            },
            new()
            {
                FileType = File.Type.DOCX,
                Count = 5
            },
            new()
            {
                FileType = File.Type.PPTX,
                Count = 2
            },
            new()
            {
                FileType = File.Type.XLSX,
                Count = 3
            },
            new()
            {
                FileType = File.Type.ZIP,
                Count = 5
            },
        };

        Stage stage3 = new()
        {
            Next = stage2,
            Folders = new()
            {
                new()
                {
                    Name = "images",
                    Files = file3images
                },
                new()
                {
                    Name = "musics",
                    Files = file3musics
                },
                new()
                {
                    Name = "documents",
                    Files = file3documents
                },
                new()
                {
                    Name = "videos",
                },
                new()
                {
                    Name = "desktop",
                },
            },
        };

        Stage stage4 = new()
        {
            Next = stage3,
            Folders = new()
            {
                new()
                {
                    Name = "golf",
                },
                new()
                {
                    Name = "summer",
                },
                new()
                {
                    Name = "winter",
                },
                new()
                {
                    Name = "football",
                },
                new()
                {
                    Name = "games",
                },
                new()
                {
                    Name = "food",
                },
                new()
                {
                    Name = "wtf",
                },
                new()
                {
                    Name = "lol",
                },
                new()
                {
                    Name = "memes",
                },
                new()
                {
                    Name = "homework",
                },
                new()
                {
                    Name = "privatedontopenplease",
                },
                new()
                {
                    Name = "waitbecarefulbeforeopenning",
                },
            },
        };

        Stage stage5 = new()
        {
            Next = stage4,
            Folders = new()
            {
                new()
                {
                    Name = "1996",
                },
                new()
                {
                    Name = "1997",
                },
                new()
                {
                    Name = "2000",
                },
                new()
                {
                    Name = "2003",
                },
                new()
                {
                    Name = "2023",
                },
                new()
                {
                    Name = "2187",
                },
                new()
                {
                    Name = "2335",
                },
                new()
                {
                    Name = "-0",
                },
                new()
                {
                    Name = "-12",
                },
            },
        };

        stage1.Previous = stage2;
        stage2.Previous = stage3;
        stage3.Previous = stage4;
        stage4.Previous = stage5;
        return stage1;
    }
}
