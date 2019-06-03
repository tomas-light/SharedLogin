export class AccountDTO {
    public id: string;
    public email: string;
    public name: string;
    public avatar: string;
    public roleId: string;
    public roleName: string;

    constructor() {
        this.id = "0";
        this.email = "";
        this.name = "";
        this.avatar = "";
        this.roleId = "";
        this.roleName = "";
    }
}