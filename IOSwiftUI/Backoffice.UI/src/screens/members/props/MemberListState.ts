import BOMemberModel from "../models/BOMemberModel";

class MemberListState {

    count: number;
    memberList: BOMemberModel[];

    constructor() {
        this.count = 0;
        this.memberList = [];
    }
}

export default MemberListState;
