export class Inventory {
  public constructor(
    public id: number | null,
    public name: string,
    public image: string,
    public description: string,
    public stock: number,
    public price: number,
    public categoryId: number | null,
    public categoryName: string,
    public warehouseId: number | null,
    public IsActive: boolean | null,
    public createdAt: Date | null,
    public updatedAt: Date | null
  ) {}
}
