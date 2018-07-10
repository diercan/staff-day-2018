import { Url } from '../core';
import { SETTINGS } from './links-settings';

export function getAvatarLink(avatarId: string, settings: typeof SETTINGS = SETTINGS): Url {
    return new Url(`${settings.baseUrl}/assets/avatars/${avatarId}`);
}
