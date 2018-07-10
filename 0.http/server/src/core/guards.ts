import { ClientError } from './errors';

export function guardAgainstEmptyArgument(getArgument: () => string): void {
    if (!getArgument()) {
        const argumentName = getArgument.toString().substr(6);
        throw new EmptyArgument(argumentName);
    }
}

export class EmptyArgument extends ClientError {
    constructor(argumentName: string) {
        super(`Argument ${argumentName} is empty`);
    }
}

export function guardAgainstInvalidEnumArgument(enumType: { [key: number]: string }, getArgument: () => number, ) {
    const argumentValue = getArgument();
    if (argumentValue !== undefined && argumentValue !== null && !(enumType[argumentValue])) {
        const argumentName = getArgument.toString().substr(6);
        throw new InvalidEnumArgument(argumentName, argumentValue);
    }
}

export class InvalidEnumArgument extends ClientError {
    constructor(argumentName: string, argumentValue: number) {
        super(`'${argumentValue}' is not a valid value for argument ${argumentName}`);
    }
}