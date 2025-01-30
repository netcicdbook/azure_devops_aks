db = db.getSiblingDB('AuditoryDB');
db.createUser(
    {
        user: "admin",
        pwd: "admin",
        roles: [
            {
                role: "readWrite",
                db: "AuditoryDB"
            }
        ]
    }
);
db.createCollection('UserRecord');
