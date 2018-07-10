import { CollectionResource, SingleResource } from './common';

export * from './common';
export type Resource<T> = T extends Array<infer U> ? CollectionResource<U> : SingleResource<T>;
