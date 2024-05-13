export class Warehouse {
  public constructor(
    public id: number,
    public name: string,
    public address: string,
    public cityId: number,
    public city: string,
    public state: string,
    public country: string,
    public isActive: boolean,
    public createdAt: Date,
    public updatedAt: Date
  ) {}
}
