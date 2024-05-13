export class User {
  public constructor(
    public id: number,
    public name: string,
    public email: string,
    public password: string,
    public phone: string,
    public roleId: number,
    public addressId: number | null,
    public licenseNumber: string | null,
    public wareHouseId: string | null,
    public isAvailable: boolean | null,
    public isApproved: number,
    public isActive: boolean,
    public createdAt: Date | null,
    public updatedAt: Date | null
  ) {}
}
