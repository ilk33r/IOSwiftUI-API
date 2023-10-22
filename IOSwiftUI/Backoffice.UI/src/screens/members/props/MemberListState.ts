import BOMemberModel from "../models/BOMemberModel";

class MemberListState {

    count: number;
    members: BOMemberModel[];

    constructor() {
        this.count = 0;
        this.members = [];
    }
}

export default MemberListState;
