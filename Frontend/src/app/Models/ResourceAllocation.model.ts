export class ResourceAllocation {

    public constructor(
        public id: number,
        public orderId: number,
        public driverId: number,
        public managerId: number,
        public vehicleId: number,
        public vehicleType:string,
        public vehicleNumber: string,
        public assignedDate: Date,
        public assignmentStatusId: number = 1 ,
        public assignmentStatus: string,
        public isActive: boolean = true,
        public createdAt: Date ,
        public updatedAt: Date 
    ) { }
}


