import { HttpStatusCode } from "@angular/common/http";

export class ApiResponse{
    public constructor(public success:boolean,public statusCode:HttpStatusCode, public data:any[]|any,  public error:string){}
}