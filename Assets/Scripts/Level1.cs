using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : Level
{
    public override Stage CreateStage()
    {
        Stage stage1 =
            new()
            {
                Next = null,
                Folders = new()
                {
                    new()
                    {
                        Name = "home",
                        Files = new()
                        {
                            new() { FileType = File.Type.TORRENT, Count = 5 },
                            new() { FileType = File.Type.GITIGNORE, Count = 5 },
                            new() { FileType = File.Type.SH, Count = 5 },
                            new() { FileType = File.Type.C, Count = 5 },
                            new() { FileType = File.Type.H, Count = 5 }
                        }
                    }
                },
            };

        List<File> files2 =
            new()
            {
                new() { FileType = File.Type.TXT, Count = 5 },
                new() { FileType = File.Type.PDF, Count = 2 },
                new() { FileType = File.Type.PPTX, Count = 3 },
                new() { FileType = File.Type.DOCX, Count = 2 },
                new() { FileType = File.Type.XLSX, Count = 2 },
                new() { FileType = File.Type.ZIP, Count = 5 },
                new() { FileType = File.Type.TORRENT, Count = 2 }
            };

        Stage stage2 =
            new()
            {
                Next = stage1,
                Folders = new()
                {
                    new() { Name = "alex", Files = files2 },
                    new() { Name = "jonathan", Files = files2 },
                    new() { Name = "edouard", Files = files2 },
                    new() { Name = "lisar", Files = files2 },
                    new() { Name = "farien", Files = files2 },
                    new() { Name = "julienne", Files = files2 },
                    new() { Name = "lulu", Files = files2 },
                    new() { Name = "brenain", Files = files2 },
                    new() { Name = "gabstophe", Files = files2 },
                },
            };

        List<File> files3images =
            new()
            {
                new() { FileType = File.Type.PNG, Count = 10 },
                new() { FileType = File.Type.JPEG, Count = 10 },
            };

        List<File> files3musics =
            new()
            {
                new() { FileType = File.Type.MP3, Count = 10 },
                new() { FileType = File.Type.ZIP, Count = 10 },
            };

        List<File> files3documents =
            new()
            {
                new() { FileType = File.Type.PDF, Count = 5 },
                new() { FileType = File.Type.DOCX, Count = 5 },
                new() { FileType = File.Type.PPTX, Count = 2 },
                new() { FileType = File.Type.XLSX, Count = 3 },
                new() { FileType = File.Type.ZIP, Count = 5 },
            };

        List<File> files3videos =
            new()
            {
                new() { FileType = File.Type.AVI, Count = 10 },
                new() { FileType = File.Type.ZIP, Count = 10 },
            };

        List<File> files3desktop =
            new()
            {
                new() { FileType = File.Type.CS, Count = 5 },
                new() { FileType = File.Type.HTML, Count = 5 },
                new() { FileType = File.Type.CSS, Count = 2 },
                new() { FileType = File.Type.C, Count = 3 },
                new() { FileType = File.Type.H, Count = 3 },
                new() { FileType = File.Type.TXT, Count = 2 },
            };

        Stage stage3 =
            new()
            {
                Next = stage2,
                Folders = new()
                {
                    new() { Name = "images", Files = files3images },
                    new() { Name = "musics", Files = files3musics },
                    new() { Name = "documents", Files = files3documents },
                    new() { Name = "videos", Files = files3videos },
                    new() { Name = "desktop", Files = files3desktop },
                },
            };

        List<File> files4 =
            new()
            {
                new() { FileType = File.Type.PNG, Count = 3 },
                new() { FileType = File.Type.JPEG, Count = 3 },
                new() { FileType = File.Type.ZIP, Count = 2 },
                new() { FileType = File.Type.DOCX, Count = 2 },
                new() { FileType = File.Type.SH, Count = 2 },
                new() { FileType = File.Type.TXT, Count = 5 },
                new() { FileType = File.Type.PDF, Count = 3 },
            };

        Stage stage4 =
            new()
            {
                Next = stage3,
                Folders = new()
                {
                    new() { Name = "golf", Files = files4 },
                    new() { Name = "summer", Files = files4 },
                    new() { Name = "winter", Files = files4 },
                    new() { Name = "football", Files = files4 },
                    new() { Name = "games", Files = files4 },
                    new() { Name = "food", Files = files4 },
                    new() { Name = "wtf", Files = files4 },
                    new() { Name = "lol", Files = files4 },
                    new() { Name = "memes", Files = files4 },
                    new() { Name = "homework", Files = files4 },
                    new() { Name = "privatedontopenplease", Files = files4 },
                    new() { Name = "waitbecarefulbeforeopenning", Files = files4 },
                },
            };

        List<File> files5 =
            new()
            {
                new() { FileType = File.Type.C, Count = 3 },
                new() { FileType = File.Type.H, Count = 3 },
                new() { FileType = File.Type.TORRENT, Count = 2 },
                new() { FileType = File.Type.GITIGNORE, Count = 2 },
                new() { FileType = File.Type.AVI, Count = 2 },
                new() { FileType = File.Type.ZIP, Count = 5 },
                new() { FileType = File.Type.DOCX, Count = 3 },
            };

        Stage stage5 =
            new()
            {
                Next = stage4,
                Folders = new()
                {
                    new() { Name = "1996", Files = files5 },
                    new() { Name = "1997", Files = files5 },
                    new() { Name = "2000", Files = files5 },
                    new() { Name = "2003", Files = files5 },
                    new() { Name = "2023", Files = files5 },
                    new() { Name = "2187", Files = files5 },
                    new() { Name = "2335", Files = files5 },
                    new() { Name = "-0", Files = files5 },
                    new() { Name = "-12", Files = files5 },
                },
            };

        stage1.Previous = stage2;
        stage2.Previous = stage3;
        stage3.Previous = stage4;
        stage4.Previous = stage5;
        return stage1;
    }
}
