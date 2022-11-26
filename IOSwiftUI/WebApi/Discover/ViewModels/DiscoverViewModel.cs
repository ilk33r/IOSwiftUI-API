using System.Collections.Generic;
using System.Linq;
using IOSwiftUI.Common.Messages.Discover;
using IOSwiftUI.Common.Models.Base;
using IOSwiftUI.Common.Models.Discover;
using IOSwiftUI.Core.ViewModels;

namespace IOSwiftUI.WebApi.Discover.ViewModels;

public class DiscoverViewModel : ViewModel
{
    public DiscoverImagesResponseModel DiscoverImages(PaginationModel pagination)
    {
        int[] followingIds = CurrentMember.Followings
                                            .Select(f => f.FollowingMemberID)
                                            .ToArray();

        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DBContext.MemberImages
                                                .Where(i => followingIds.Contains(i.Member.ID))
                                                .Count();

        IList<DiscoverImageModel> followingImages = DBContext.MemberImages
                                                                .Select(i => new DiscoverImageModel()
                                                                {
                                                                    MemberId = i.Member.ID,
                                                                    PublicId = i.FileName,
                                                                    UserName = i.Member.UserName,
                                                                    UserNameAndSurname = i.Member.Name + " "  + i.Member.Surname,
                                                                    UserProfilePicturePublicId = i.Member.ProfilePictureFileName,
                                                                    CreateDate = i.CreateDate
                                                                })
                                                                .Where(i => followingIds.Contains(i.MemberId))
                                                                .OrderByDescending(i => i.CreateDate)
                                                                .Skip(pagination.Start)
                                                                .Take(pagination.Count)
                                                                .ToList();

        foreach(DiscoverImageModel image in followingImages)
        {
            image.PublicId = CreatePublicId(image.PublicId);
            image.UserProfilePicturePublicId = CreatePublicId(image.UserProfilePicturePublicId);
        }

        responsePagination.Count = followingImages.Count();
        return new DiscoverImagesResponseModel(followingImages, responsePagination);
    }

    public DiscoverImagesResponseModel DiscoverAllImages(PaginationModel pagination)
    {
        PaginationModel responsePagination = new PaginationModel();
        responsePagination.Start = pagination.Start;
        responsePagination.Total = DBContext.MemberImages
                                                .Count();

        IList<DiscoverImageModel> followingImages = DBContext.MemberImages
                                                                .Select(i => new DiscoverImageModel()
                                                                {
                                                                    MemberId = i.Member.ID,
                                                                    PublicId = i.FileName,
                                                                    UserName = i.Member.UserName,
                                                                    UserNameAndSurname = i.Member.Name + " "  + i.Member.Surname,
                                                                    UserProfilePicturePublicId = i.Member.ProfilePictureFileName,
                                                                    CreateDate = i.CreateDate
                                                                })
                                                                .OrderByDescending(i => i.CreateDate)
                                                                .Skip(pagination.Start)
                                                                .Take(pagination.Count)
                                                                .ToList();

        foreach(DiscoverImageModel image in followingImages)
        {
            image.PublicId = CreatePublicId(image.PublicId);
            image.UserProfilePicturePublicId = CreatePublicId(image.UserProfilePicturePublicId);
        }
        
        responsePagination.Count = followingImages.Count();
        return new DiscoverImagesResponseModel(followingImages, responsePagination);
    }
}
