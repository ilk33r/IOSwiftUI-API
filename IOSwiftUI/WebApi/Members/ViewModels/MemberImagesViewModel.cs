using System;
using System.Collections.Generic;
using System.Linq;
using IOBootstrap.NET.Common.Exceptions.Members;
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
        DBContext.Attach(member);

        ImagesEntity memberImage = new ImagesEntity()
        {
            FileName = fileName,
            CreateDate = DateTime.UtcNow,
            Member = member
        };

        DBContext.Add(memberImage);
        DBContext.SaveChanges();
    }

    public void DeleteProfilePicture()
    {
        if (String.IsNullOrEmpty(CurrentMember.ProfilePicturePublicId))
        {
            throw new ImageNotFoundException();
        }

        string fileName = GetFileName(CurrentMember.ProfilePicturePublicId);
        RemoveFile(fileName);
    }

    public void UpdateMemberProfilePicture(string fileName)
    {
        if (!String.IsNullOrEmpty(CurrentMember.ProfilePicturePublicId))
        {
            throw new MemberHasProfilePictureException();
        }

        MemberEntity member = DBContext.Members.Find(CurrentMember.ID);
        if (member == null)
        {
            RemoveFile(fileName);
            throw new IOUserNotFoundException();
        }

        member.ProfilePictureFileName = fileName;
        DBContext.Update(member);
        DBContext.SaveChanges();
    }

    public MemberImagesResponseModel MemberImages(PaginationModel pagination)
    {
        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DBContext.MemberImages
                                                .Where(i => i.Member.ID == CurrentMember.ID)
                                                .Count();

        List<MemberImageModel> memberImages = DBContext.MemberImages
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
}
