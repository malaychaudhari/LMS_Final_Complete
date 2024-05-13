export class Vehicle{
    public constructor(public id:number, public vehicleTypeId:number, public vehicleType:string, public vehicleNumber:string , public wareHouseId:number, public isAvailable:boolean, public isActive:boolean | null, public createdAt:Date | null, public updatedAt:Date | null){}
}