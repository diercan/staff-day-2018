import { Server } from 'restify';

export default function bootstrap(server: Server) {
    server.get('/assets/avatars/:id', (req, res, next) => {
        res.setHeader('Location', `https://randomuser.me/api/portraits/men/${Math.round(1 + Math.random() * 98)}.jpg`);
        res.send(301);
        next();
    });
}
