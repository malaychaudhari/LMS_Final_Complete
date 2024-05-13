export class AdminStatistics {

    public constructor(
        public warehousesCount: number,
        public managersCount: number,
        public driversCount: number,
        public customersCount: number,
        public totalOrdersCount: number,
        public totalSalesAmount: number) {}
}
