export interface IValidationResult<T> extends Promise<T> {
    value: T;
    error: any;
}
