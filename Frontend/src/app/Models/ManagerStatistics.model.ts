export class ManagerStatisticsModel {
  public constructor(
    public inventoryCount: number,
    public inventoryCategoryCount: number,
    public vehicleCount: number,
    public availableVehicleCount: number,
    public vehicleTypeCount: number,
    public driverCount: number,
    public availableDriverCount: number,
    public orderCount: number,
    public pendingOrderCount: number
  ) {}
}
