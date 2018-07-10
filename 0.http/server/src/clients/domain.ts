import { guardAgainstEmptyArgument, guardAgainstInvalidEnumArgument } from '../core';

export class Client {
    constructor(
        public id: string,

        public firstName: string,
        public lastName: string,

        public country: Country,
        public postalCode: string) {
            guardAgainstEmptyArgument(() => id);
            guardAgainstEmptyArgument(() => firstName);
            guardAgainstEmptyArgument(() => lastName);
            guardAgainstInvalidEnumArgument(Country, () => country);
        }

        static fromObject(o: any): Client {
            return new Client(o.id, o.firstName, o.lastName, o.country, o.postalCode);
        }
}

export enum Country {
    Germany,
    UnitedKingdom
}
