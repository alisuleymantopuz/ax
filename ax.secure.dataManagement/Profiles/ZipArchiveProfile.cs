using System;
using AutoMapper;
using ax.secure.dataManagement.Models;
using ax.storage;

namespace ax.secure.dataManagement.Profiles
{
    /// <summary>
    /// Zip archive profile.
    /// </summary>
    public class ZipArchiveProfile :Profile
    {
        public ZipArchiveProfile()
        {
            CreateMap<ZipArchive, ZipArchiveModel>().ReverseMap();
        }
    }
}
