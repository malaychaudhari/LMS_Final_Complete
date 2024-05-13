export class Order {
    public constructor(public id: number, public customerId: number, public userName: string, public orderDate: Date, public totalAmount: number, public orderDetailId: number, public inventoryId: number, public inventoryName: string, public quantity: number, public subTotal: number, public orderStatusId: number, public originId: number, public destinationId: number, public statusId: number,
        public status: string | null, public isActive: boolean, public createdAt: Date, public updatedAt: Date) { }
}
