public class Path {
	ArrayList nodes;
	public Path() {
		nodes = new ArrayList();
	}
	public Path(Vector2 start) {
		nodes = new ArrayList();
		nodes.Add(new Node(start));
	}
	public void AddNode(Vector2 pos) {
		AddNode(new Node(pos));
	}
	public void AddNode(Node n) {
		if (nodes.Count != 0) {
			((Node)nodes[nodes.Count-1]).SetNextNode(n);
			n.SetPreviousNode((Node)nodes[nodes.Count-1]);
		}
		nodes.Add(n);
	}
	public void ModifyNode(int i, Vector2 position) {
		((Node)nodes[i]).SetPosition(position);
	
	}
	public Node GetNode(int i) {
		return (Node)nodes[i];
	}
	public Vector2 GetPosition(float f) {
		int n = (int)f;
		float p = f - (float)n;
		if (nodes.Count > n + 1) {
			
			Vector2 p1 = ((Node)nodes[n]).GetNextOffset(p);
			Vector2 p2 = ((Node)nodes[n + 1]).GetPreviousOffset(1.0f - p);
			Vector2 dp = p2 - p1;
			dp *= adjustPrecent(p);
			dp += p1;
			
			return dp;
		}
		return default(Vector2);
	}
	private float adjustPrecent(float x) {
		return 1.0f - ((float)Math.Cos(x * 3.1415f)/2.0f + 0.5f);
	}
	public int GetSize() {
		return nodes.Count;
	}
}
public class Node {
	private Vector2 pos, nVec, pVec;
	private Node nNode, pNode;
	private float angle, pDist, nDist;
	public Node(Vector2 position) {
		pos = position;
		pDist = 0.0f;
		nDist = 0.0f;
		angle = 0.0f;
	}
	public void SetNextNode(Node n) {
		nNode = n;
		recalc();
	}
	public void SetPreviousNode(Node n) {
		pNode = n;
		recalc();
	}
	public Vector2 GetPosition() {
		return pos;
	}
	private void recalc() {
		bool pNull = (pNode == null);
		bool nNull = (nNode == null);
		if (pNull && nNull) {
			angle -= 1.57075f;
		} else if (pNull) {
			Vector2 dif = nNode.GetPosition() - pos;
			angle = (float)Math.Atan2(dif.Y, dif.X);
		} else if (nNull) {
			Vector2 dif = pos - pNode.GetPosition();
			angle = (float)Math.Atan2(dif.Y, dif.X);
			//angle -= 1.57075f;
		} else {
			Vector2 pDif = pNode.GetPosition() - pos;
			Vector2 nDif = nNode.GetPosition() - pos;
			float pAng = (float)Math.Atan2(pDif.Y, pDif.X);
			float nAng = (float)Math.Atan2(nDif.Y, nDif.X);
			
			float aAng = avgAngle(pAng, nAng);
			
			float paDif = difAngle(nAng, aAng - 1.57075f);
			float naDif = difAngle(nAng, aAng + 1.57075f);
			if (paDif < naDif)
				aAng -= 1.57075f;
			else
				aAng += 1.57075f;
			
			angle = aAng;
		}
		
		
		if (!pNull)
			pDist = getDist(pNode);
		if (!nNull)
			nDist = getDist(nNode);
			
		nVec = new Vector2((float)Math.Cos(angle        )*nDist, (float)Math.Sin(angle        )*nDist);
		pVec = new Vector2((float)Math.Cos(angle+3.1415f)*pDist, (float)Math.Sin(angle+3.1415f)*pDist);
	}
	public void SetPosition(Vector2 position) {
		pos = position;
		recalc();
		if (nNode != null)
			nNode.ForceUpdate();
		if (pNode != null)
			pNode.ForceUpdate();
	}
	public void ForceUpdate() {
		recalc();
	}
	public Vector2 GetNextOffset(float f) {
		return pos + nVec*f;
	}
	public Vector2 GetPreviousOffset(float f) {
		return pos + pVec*f;
	}
	private float avgAngle(float a1, float a2) {
		float dif = (float)Math.Abs(a1 - a2);
		if (dif > 3.141592f) {
			if (a2 > a1) {
				float e = a1;
				a1 = a2;
				a2 = e;
			}
			a1 -= 6.28f;
		}
		return (a1 + a2)/2.0f;
	}
	private float difAngle(float a1, float a2) {
		float dif = (float)Math.Abs(a1 - a2);
		if (dif > 3.141592f) {
			if (a2 > a1) {
				float e = a1;
				a1 = a2;
				a2 = e;
			}
			a1 -= 6.28f;
		}
		return (float)Math.Abs(a1 - a2);
	}
	private float getDist(Node n) {
		Vector2 d = pos - n.GetPosition();
		return (float)Math.Sqrt(d.X * d.X + d.Y * d.Y);
	}
}