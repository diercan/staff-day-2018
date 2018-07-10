import { Server } from 'restify';
import { getAvatarLink } from './links';
import { SETTINGS } from './links-settings';

export default function bootstrap(server: Server) {
    SETTINGS.baseUrl = server.url;

    server.get(getAvatarLink(':id', {
        baseUrl: ''
    }).toString(), (req, res, next) => {
        res.setHeader('Location', `https://randomuser.me/api/portraits/men/${Math.round(1 + Math.random() * 98)}.jpg`);
        res.send(301);
        next();
    });
}
