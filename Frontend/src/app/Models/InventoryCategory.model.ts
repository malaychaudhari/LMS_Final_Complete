export class InventoryCategory {
  public constructor(
    public id: number,
    public name: string,
    public IsActive: boolean,
    public createdAt: Date,
    public updatedAt: Date
  ) {}
}
