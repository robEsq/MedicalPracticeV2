fetch('https://localhost:7290/api/User')
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(() => {
        console.log("Failed to get Users")
    });

    // figure out how to map to a list
const User = () => {

    return (
        <div className="User">
            <p>
                Hey you!!!
            </p>
        </div>
    );
}

export default User;
