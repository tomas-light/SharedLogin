export class LoginDTO {
    public email: string;
    public password: string;

    constructor(dto: LoginDTO = null) {
        if (dto) {
            this.email = dto.email;
            this.password = dto.password;
        }
        else {
            this.email = "";
            this.password = "";
        }
    }
}