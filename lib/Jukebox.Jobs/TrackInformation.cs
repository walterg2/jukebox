using System;
using Id3;
using Id3.Frames;
using Jukebox.Jobs.Extensions;

namespace Jukebox.Jobs
{
    public class TrackInformation
    {
        private TrackInformation(string filePath)
        {
            using (var mp3 = new Mp3File(filePath))
            {
                var tag = mp3.GetTag(Id3TagFamily.FileStartTag) ?? new NullFrame();
                Artist = ValueFor(tag.Artists);
                Album = ValueFor(tag.Album);
                Title = ValueFor(tag.Title);
                Year = ValueFor(tag.Year);
            }
        }

        public static TrackInformation For(string filePath)
        {
            return new TrackInformation(filePath);
        }

        public string Artist { get; private set; }

        public string Album { get; private set; }

        public string Year { get; private set; }

        public string Title { get; private set; }

        public string Path
        {
            get { return string.Format("{0}/{1}", Artist, Album).Sanitize(); }
        }

        private static string ValueFor(TextFrame frame)
        {
            return null != frame ? (frame.Value ?? String.Empty) : string.Empty;
        }

        public string FileName
        {
            get { return Title.Sanitize() + ".mp3"; }
        }

        private class NullFrame : Id3Tag
        {
        }
    }
}