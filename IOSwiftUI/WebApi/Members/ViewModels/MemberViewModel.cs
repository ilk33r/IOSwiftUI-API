using System;
using IOSwiftUI.Common.Models;
using IOSwiftUI.Core.ViewModels;

namespace IOSwiftUI.WebApi.Members.ViewModels;

public class MemberViewModel : ViewModel
{
    public MemberModel GetCurrentMember()
    {
        MemberModel currentMember = CurrentMember;
        return currentMember;
    }
}
