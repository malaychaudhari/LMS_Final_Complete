export class Address {

    public constructor(
        public id: number,
        public userId: number,
        public address: string,
        public pincode: string,
        public cityId: number,
        public city?: string,
        // public stateId?: number,
        public state?: string,
        // public countryId?: number,
        public country?: string,
        public isActive?: boolean,
        public createdAt?: Date,
        public updatedAt?: Date,
        ) {}
}
