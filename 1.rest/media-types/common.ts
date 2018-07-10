export type CollectionResource<T> = {
    resources: SingleResource<T>[];
    links: Link[];
};

export type SingleResource<T> = {
    properties: T;
    links: Link[];
};

export type Link = {
    title?: string;

    relation: LinkRelation;
    href: string;
    allow: LinkAllow[];
};

export type LinkRelation = 'self' | 'icon' | 'create';
export type LinkAllow = 'read' | 'delete' | 'update' | 'create';
