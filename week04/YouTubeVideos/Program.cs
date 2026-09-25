using System;

class Program
{
    static void Main(string[] args)
    {
        Video video1 = new Video("How to Cook Pasta", "John Doe", 300);
        video1.AddComment(new Comment("Alice", "Great tutorial!"));
        video1.AddComment(new Comment("Bob", "Thanks for the tips!"));
        video1.DisplayInfo();
        Video video2 = new Video("Learn C# in 10 Minutes", "Jane Smith", 600);
        video2.AddComment(new Comment("Charlie", "Very helpful!"));
        video2.AddComment(new Comment("David", "I learned a lot!"));
        video2.DisplayInfo();
        Video video3 = new Video("Top 10 Travel Destinations", "Emily Johnson", 900);
        video3.AddComment(new Comment("Frank", "I want to visit all of these places!"));
        video3.AddComment(new Comment("Grace", "Great recommendations!"));
        video3.DisplayInfo();
    }
}