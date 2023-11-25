enum UserStatuses {

    Active = 0,
    Deactivated = 1,
    TemporaryDisabled = 2,
    Banned = 3,

}

namespace UserStatuses {

    export function getUserStatusesName(type: UserStatuses): string {
        if (type === UserStatuses.Active) {
            return "Active";
        }

        if (type === UserStatuses.Deactivated) {
            return "Deactivated";
        }

        if (type === UserStatuses.TemporaryDisabled) {
            return "TemporaryDisabled";
        }

        if (type === UserStatuses.Banned) {
            return "Banned";
        }

        return "Banned";
    }

}

export default UserStatuses;
