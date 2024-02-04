using System;
using System.Collections.Generic;
using System.Linq;
using IOBootstrap.NET.Common.Cache;
using IOBootstrap.NET.Common.Exceptions.Members;
using IOSwiftUI.Common.Constants;
using IOSwiftUI.Common.Exceptions;
using IOSwiftUI.Common.Messages.Members;
using IOSwiftUI.Common.Models.Base;
using IOSwiftUI.Common.Models.Members;
using IOSwiftUI.Core.ViewModels;
using IOSwiftUI.DataAccess.Entities;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberImagesViewModel : ImageViewModel
{

    public void AddMemberImage(string fileName)
    {
        MemberEntity member = new MemberEntity()
        {
            ID = CurrentMember.ID
        };
        DatabaseContext.Attach(member);

        ImagesEntity memberImage = new ImagesEntity()
        {
            FileName = fileName,
            CreateDate = DateTime.UtcNow,
            IsDraft = true,
            Member = member
        };

        DatabaseContext.Add(memberImage);
        DatabaseContext.SaveChanges();
    }

    public void DeleteProfilePicture()
    {
        if (String.IsNullOrEmpty(CurrentMember.ProfilePicturePublicId))
        {
            throw new ImageNotFoundException();
        }


        MemberEntity member = DatabaseContext.Members.Find(CurrentMember.ID);
        if (member == null)
        {
            throw new IOUserNotFoundException();
        }

        string fileName = GetFileName(CurrentMember.ProfilePicturePublicId);
        RemoveFile(fileName);

        member.ProfilePictureFileName = null;
        DatabaseContext.Update(member);
        DatabaseContext.SaveChanges();

        string cacheKey = String.Format(CacheKeys.UserCacheKey, CurrentMember.ID);
        IOCache.InvalidateCache(cacheKey);
    }

    public void UpdateMemberProfilePicture(string fileName)
    {
        if (!String.IsNullOrEmpty(CurrentMember.ProfilePicturePublicId))
        {
            throw new MemberHasProfilePictureException();
        }

        MemberEntity member = DatabaseContext.Members.Find(CurrentMember.ID);
        if (member == null)
        {
            RemoveFile(fileName);
            throw new IOUserNotFoundException();
        }

        member.ProfilePictureFileName = fileName;
        DatabaseContext.Update(member);
        DatabaseContext.SaveChanges();

        string cacheKey = String.Format(CacheKeys.UserCacheKey, CurrentMember.ID);
        IOCache.InvalidateCache(cacheKey);
    }

    public MemberImagesResponseModel GetCurrentMemberImages(PaginationModel pagination)
    {
        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DatabaseContext.MemberImages
                                                .Where(i => i.Member.ID == CurrentMember.ID)
                                                .Count();

        List<MemberImageModel> memberImages = DatabaseContext.MemberImages
                                                            .Select(i => new MemberImageModel()
                                                            {
                                                                ImageId = i.ID,
                                                                MemberId = i.Member.ID,
                                                                PublicId = i.FileName,
                                                                CreateDate = i.CreateDate
                                                            })
                                                            .Where(i => i.MemberId == CurrentMember.ID)
                                                            .OrderByDescending(i => i.CreateDate)
                                                            .Skip(pagination.Start)
                                                            .Take(pagination.Count)
                                                            .ToList();

        foreach(MemberImageModel image in memberImages)
        {
            image.PublicId = CreatePublicId(image.PublicId);
        }

        responsePagination.Count = memberImages.Count();
        return new MemberImagesResponseModel(memberImages, responsePagination);
    }

    public MemberImagesResponseModel GetOtherMemberImages(PaginationModel pagination, string userName)
    {
        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DatabaseContext.MemberImages
                                                .Where(i => i.Member.UserName.ToLower().Equals(userName))
                                                .Count();

        List<MemberImageModel> memberImages = DatabaseContext.MemberImages
                                                            .Select(i => new MemberImageModel()
                                                            {
                                                                ImageId = i.ID,
                                                                MemberId = i.Member.ID,
                                                                PublicId = i.FileName,
                                                                UserName = i.Member.UserName,
                                                                CreateDate = i.CreateDate
                                                            })
                                                            .Where(i => i.UserName.ToLower().Equals(userName))
                                                            .OrderByDescending(i => i.CreateDate)
                                                            .Skip(pagination.Start)
                                                            .Take(pagination.Count)
                                                            .ToList();

        foreach(MemberImageModel image in memberImages)
        {
            image.PublicId = CreatePublicId(image.PublicId);
            image.UserName = null;
        }

        responsePagination.Count = memberImages.Count();
        return new MemberImagesResponseModel(memberImages, responsePagination);
    }
}
